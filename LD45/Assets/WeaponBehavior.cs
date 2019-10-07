﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    bool IsRunning = false;
    SpriteRenderer SR;
    public float AttackTime = 0.5F;
    private float currentTime = 0F;

    public Vector3 StartingPosition;
    public Quaternion StartingRotation;

    public void Attack()
    {
        IsRunning = true;
        // Launch Animation
        SR.enabled = true;
        currentTime = 0F;
        transform.localPosition = StartingPosition;
        transform.localRotation = StartingRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Monster apply dmg
        SR.enabled = false;
        currentTime = 0;
        IsRunning = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        StartingRotation = transform.localRotation;
        StartingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRunning)
        {
            currentTime += Time.deltaTime;
            if( currentTime > AttackTime)
            {
                SR.enabled = false;
                IsRunning = false;
                currentTime = 0; 
            }
        }   
    }
}
