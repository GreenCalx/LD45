using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private WorldBuilder worldBuilder;

    // Main Layers modifications
    public int world_state = 0;

    // Secondary ( Graphical ) layers modifications
    public int world_substate = 0;

    public void upgrade( bool isMajorUpgrade )
    {
        worldBuilder = GetComponent<WorldBuilder>();
        if (!!worldBuilder)
        {
            if (isMajorUpgrade)
            {
                world_state++;
                worldBuilder.build_major(world_state);
            }
            else
            {
                world_substate++;
                worldBuilder.build_minor(world_substate);
            }
        }
    }

    /// ------------ UNITY ------------

    // Start is called before the first frame update
    void Start()
    {
        world_state     = 0;
        world_substate  = 0;
        worldBuilder = this.GetComponent<WorldBuilder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
