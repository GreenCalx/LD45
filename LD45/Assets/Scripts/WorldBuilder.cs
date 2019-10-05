using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public GameObject gridWorld0GO;
    public GameObject gridWorld1GO;
    public GameObject gridWorld2GO;

    private GameObject currentGridWorld;

    public void build( uint major_state, uint minor_state)
    {

        // MAJOR STATE
        switch (major_state)
        {
            case 1:
                currentGridWorld = gridWorld1GO;
                break;
            case 2:
                currentGridWorld = gridWorld2GO;
                break;
            default:
                break;
        }

        // MINOR STATE


    }




    /// ------------ UNITY ------------

    // Start is called before the first frame update
    void Start()
    {
        if (!!gridWorld0GO)
            currentGridWorld = gridWorld0GO;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
