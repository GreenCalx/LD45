Shader "Unlit/TestRippleEffect" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Annulus("Annulus Radius", Float) = 1 // An annulus is the 2D version of a Torus. This is the inside radius, in local coordinates
		_MaxRange("Outer Radius", Float) = 1 //outer radius, in local coordinates
		_DistortionStrength("DistortionStrength", Float) = 1
	}
		SubShader{
			Pass {
				CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				uniform sampler2D _MaskTex;

				half _Annulus;
				half _MaxRange;
				half _DistortionStrength;

				fixed4 frag(v2f_img i) : COLOR {

					half dist = length(i.uv - 0.5);
					dist = saturate((dist - _MaxRange + _Annulus) / (_Annulus)); //interpolation value with zero as the inside edge of the annulus and 1 as the outside edge

				   if (dist > 0 && dist < 1)
				   {
					   dist = dist * dist; //nonlinear distribution, so it's not just magnifying stuff. Also makes the transition smooth on the inside of the annulus, but sharp on the outside
					   return tex2D(_MainTex, (i.uv-0.5) + dist * _DistortionStrength * normalize(i.uv - 0.5)); //our uv, but shifted outwards (in local space)
				   }
				   else
				   {
					   return tex2D(_MainTex, i.uv); //no distortion
				   }
				}
				ENDCG
			}
		}
}
