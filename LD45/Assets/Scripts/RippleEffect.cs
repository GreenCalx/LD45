using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RippleEffect : MonoBehaviour
{
    public float annulus;
    public float range;
    public float strength;
    private Material material;

    public float length_in_s;
    private float current_time;
    void Awake()
    {
        material = new Material(Shader.Find("Unlit/TestRippleEffect"));
        current_time = 0;
    }

    private void Update()
    {
        current_time += Time.deltaTime;
        if(current_time > length_in_s)
        {
            current_time = 0;
            this.enabled = false;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Annulus", annulus );
        material.SetFloat("_MaxRange", range * current_time/length_in_s);
        material.SetFloat("_DistortionStrength", strength);
        Graphics.Blit(source, destination, material);
    }
}
