using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEvents : MonoBehaviour
{
   
    // Upgrader GOs
    public GameObject majorUpgraderGO;
    public GameObject minorUpgraderGO;

    // Upgrade Locations
    public Transform loc_maj_upgrade0;
    public Transform loc_maj_upgrade1;
    public Transform loc_maj_upgrade2;


    ///  MAPPING MAJOR UPGRADES
    //
    //  [0]     PLAYER SPAWN        player_acquired()
    //  [1]     VOID COLLISION      void_collision_acquired()
    //  [2]     ALL COLLISION       all_collision_acquired()
    //  [3]     TONGUE ACQUIR.      tongue_acquired()
    //


    // player spawns a white square [UPGRADE 0]
    public void player_acquired()
    {
        if (!!majorUpgraderGO)
        {
            GameObject new_upgrade = Instantiate(majorUpgraderGO);
            new_upgrade.transform.position = loc_maj_upgrade0.position;
        }
    }


    /// ------------ UNITY ------------

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}

