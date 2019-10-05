using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RippleEffect : MonoBehaviour
{
    public float annulus;
    public float range;
    public float strength;
    private Material material;

    private bool first_time = true;

    public float length_in_s;
    private float current_time;

    public Texture2D Background;

    void Awake()
    {
        material = new Material(Shader.Find("Unlit/TestRippleEffect"));
        current_time = 0;
        first_time = true;
    }

    private void Start()
    {
        current_time = 0;
        first_time = true;
    }

    private void OnEnable()
    {
        current_time = 0;
        first_time = true;
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
        if (first_time)
        {
            //Get last frame
            var current = RenderTexture.active;

            RenderTexture.active = source;
            Background = new Texture2D(source.width, source.height);
            Background.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
            Background.Apply();

            RenderTexture.active = current;

            first_time = false;
        }
        material.SetTexture("_Background", Background);
        material.SetFloat("_Annulus", annulus );
        material.SetFloat("_MaxRange", range * current_time/length_in_s);
        material.SetFloat("_DistortionStrength", strength);
        Graphics.Blit(source, destination, material);       
    }
}
