using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUpgrader : MonoBehaviour
{

    public GameObject worldGO;
    public bool isMajorUpgrade;

    private World world;

    private void activate()
    {
        if (!!world)
            world.upgrade( isMajorUpgrade );
    }


    /// ------------ UNITY ------------

    // Start is called before the first frame update
    void Start()
    {
        if (!!worldGO)
            world = worldGO.GetComponent<World>();
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
