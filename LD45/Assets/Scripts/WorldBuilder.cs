using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class WorldBuilder : MonoBehaviour
{
    public GameObject gridWorld0GO;
    public GameObject gridWorld1GO;
    public GameObject gridWorld2GO;
    public GameObject gridWorld3GO;
    public GameObject gridWorld4GO;

    public GameObject currentGridWorldGO;

    public void build_major( int major_state )
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
            case 3:
                currentGridWorldGO = Instantiate(gridWorld3GO);
                break;
            case 4:
                currentGridWorldGO = Instantiate(gridWorld4GO);
                break;
            default:
                break;
        }

    }

    public void build_minor(int minor_state)
    {
        // MINOR STATE
        switch (minor_state)
        {
            case 0:
                // default voidzone
                break;
            case 1:
                // Graphical 1 voidzone
                break;
            case 2:
                // collapsed bridge voidzone repair ( deactivate subbform0 )
                // ZONE I COMPLETE
                repairBridgeVoidZone();
                break;
            case 3:
                // Graphical 1 main zone
                break;
            case 4:
                // Graphical 2 main zone
                break;
            case 5:
                // Graphical 3 main zone
                break;
            case 6:
                // LAB 1
                // Graphical 2 voidzone
                break;
            case 7:
                // LAST FROM LAB / ZONE II COMPLETE
                repairBridgeMainZone();
                break;
            case 8:
                //
                break;
            case 9:
                //
                break;
            case 10:
                //
                break;
            default:
                break;
        }
    }

    public void repairBridgeVoidZone()
    {
        GameObject go = GameObject.Find(Constants.BROKEN_BRIDGE_MAP_SUBFORM_GO_ID);
        if (!!go)
        {
            TilemapCollider2D tc2d = go.GetComponent<TilemapCollider2D>();
            Tilemap t = go.GetComponent<Tilemap>();
            if (!!tc2d && !!t)
            {
                tc2d.enabled = false;
                t.color = new Color(255f, 255f, 255f, 0f);
            }
        }
    }

    public void repairBridgeMainZone()
    {
        GameObject go = GameObject.Find(Constants.BROKEN_BRIDGE_WORLD_2_GO_ID);
        if (!!go)
        {
            TilemapCollider2D tc2d = go.GetComponent<TilemapCollider2D>();
            Tilemap t = go.GetComponent<Tilemap>();
            if (!!tc2d && !!t)
            {
                tc2d.enabled = false;
                t.color = new Color(255f, 255f, 255f, 0f);
            }
        }
    }

    /// ------------ UNITY ------------

    // Start is called before the first frame update
    void Start()
    {
        build_major( 0 );
        build_minor( 0 );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
