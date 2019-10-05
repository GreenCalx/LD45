﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Sprite))]
public class PlayerController : MonoBehaviour
{

    // World effector
    public  GameObject  worldGO;

    // Upgrade status
    public bool acquired_existence      = false;
    public bool acquired_void_collision = false;
    public bool acquired_all_collision  = false;
    public bool acquired_tongue         = false;

    public int level = 0;

    // Movements
    public bool IsControllable = false;
    public bool IsVisible = false;
    public float Speed = 100;

    // Inputs
    private float MoveX = 0;
    private float MoveY = 0;
    private bool Button_Space;
    private bool Button_Ctrl;

    // Physics
    private Rigidbody2D RB2D;
    private SpriteRenderer SR;
    private BoxCollider2D BC;


    public void levelUp()
    {
        level++;
        switch (level)
        {
            case 1:
                acquired_void_collision = true;
                break;
            case 2:
                acquired_all_collision = true;
                break;
            case 3:
                acquired_tongue = true;
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        BC = GetComponent<BoxCollider2D>();

        if(!IsVisible)
        {
            SR.enabled = false;
            BC.enabled = false;
        }

        worldGO = GameObject.Find(Constants.WORLD_GO_ID);
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        MoveX = Input.GetAxis("Horizontal");
        MoveY = Input.GetAxis("Vertical");

        Button_Space |= Input.GetButtonDown("Jump");
        Button_Ctrl |= Input.GetButtonDown("Fire1");

        BC.enabled = true;
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject.Find("Main Camera").GetComponent<CameraController>().StartRippleEffect();

            acquired_void_collision = true;
        }
        

        // > Check for player upgrade updates
        if ( Button_Space && !acquired_existence && !!worldGO)
        {
            WorldEvents worldEvents = worldGO.GetComponent<WorldEvents>();
            worldEvents.player_acquired();
            acquired_existence = true;
        }
    }//! Update

    private void FixedUpdate()
    {
        //Move
        if (IsControllable)
        {
            RB2D.velocity = new Vector2(MoveX * Time.deltaTime * Speed, MoveY * Time.deltaTime * Speed);
        }
        if(IsVisible && Button_Ctrl)
        {
            // TODO: Launch Dialog
            IsControllable = true;
        }
        if(!IsVisible && Button_Space)
        {
            // TODO: Make Sprite visible
            SR.enabled = true;
            IsVisible = true;
        }

        // Reset inputs
        Button_Ctrl = false;
        Button_Space = false;
        MoveX = 0;
        MoveY = 0;
    }
}
