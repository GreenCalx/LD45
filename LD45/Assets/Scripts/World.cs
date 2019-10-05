﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private WorldBuilder worldBuilder;

    // Main Layers modifications
    private uint world_state { get; set; }

    // Secondary ( Graphical ) layers modifications
    private uint world_substate { get; set; }

    public void upgrade( bool isMajorUpgrade )
    {
        if (isMajorUpgrade) world_state++;
        else world_substate++;

        updateWorld();
    }

    public void updateWorld()
    {
        worldBuilder = GetComponent<WorldBuilder>();
        if (!!worldBuilder)
            worldBuilder.build(world_state, world_substate);
    }

    /// ------------ UNITY ------------

    // Start is called before the first frame update
    void Start()
    {
        world_state = 0;
        world_substate = 0;
        worldBuilder = this.GetComponent<WorldBuilder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
