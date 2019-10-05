Shader "Unlit/TestRippleEffect" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Background("BackGround", 2D) = "black" {}
		_Annulus("Annulus Radius", Float) = 1 
		_MaxRange("Outer Radius", Float) = 1 
		_DistortionStrength("DistortionStrength", Float) = 1
	}
		SubShader{
			Pass {
				CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
		        uniform sampler2D _Background;

				half _Annulus;
				half _MaxRange;
				half _DistortionStrength;

				fixed4 frag(v2f_img i) : COLOR {

					half dist = length(i.uv - 0.5);
					dist = saturate((dist - _MaxRange + _Annulus) / (_Annulus)); //interpolation value with zero as the inside edge of the annulus and 1 as the outside edge

				   if (dist > 0 && dist < 1)
				   {
					   dist = dist * dist; //nonlinear distribution, so it's not just magnifying stuff. Also makes the transition smooth on the inside of the annulus, but sharp on the outside
					   return tex2D(_MainTex, (i.uv+ (0.01*dist*normalize(i.uv-0.5))*_DistortionStrength )); //our uv, but shifted outwards (in local space)
				   }
				   else if (dist > 0)
				   {
					   return tex2D(_Background, i.uv);  
				   }
				   else {
					   return tex2D(_MainTex, i.uv); //no distortion
				   }

				}
				ENDCG
			}
		}
}
