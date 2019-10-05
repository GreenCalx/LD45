﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUpgrader : MonoBehaviour
{
    // WORLD
    public GameObject worldGO;
    public bool isMajorUpgrade;

    // CAMERA
    private GameObject       cameraGO;
    private CameraController cameraController;


    private World world;

    private void activate()
    {
        Debug.Log("activate");
        if (!!world)
            world.upgrade( isMajorUpgrade );

        // Effect
        if (!!cameraController)
            cameraController.StartRippleEffect();

        // Destroy go
        Destroy(this.gameObject);
    }


    /// ------------ UNITY ------------

    // Start is called before the first frame update
    void Start()
    {
        if (!!worldGO)
            world = worldGO.GetComponent<World>();
        cameraGO = GameObject.Find(Constants.CAMERA_GO_ID);
        if (!!cameraGO)
            cameraController = cameraGO.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!!pc)
            activate();
         
    }
}
