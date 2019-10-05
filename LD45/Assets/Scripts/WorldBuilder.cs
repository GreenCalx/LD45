using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public GameObject gridWorld0GO;
    public GameObject gridWorld1GO;
    public GameObject gridWorld2GO;

    public GameObject currentGridWorldGO;

    public void build( int major_state, int minor_state)
    {
        //clear old map
        Destroy( currentGridWorldGO.gameObject );

        // MAJOR STATE
        switch (major_state)
        {
            case 0:
                currentGridWorldGO = Instantiate(gridWorld0GO);
                break;
            case 1:
                currentGridWorldGO = Instantiate(gridWorld1GO);
                break;
            case 2:
                currentGridWorldGO = Instantiate(gridWorld2GO);
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
        build( 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
