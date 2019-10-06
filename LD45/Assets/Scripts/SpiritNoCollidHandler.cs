using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpiritNoCollidHandler : MonoBehaviour
{
    public bool defaultState = true;

    GameObject playerGO;
    GameObject worldGO;
    private int stored_player_level;
    private int stored_world_level;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find(Constants.PLAYER_GO_ID);
        worldGO = GameObject.Find(Constants.WORLD_GO_ID);

        if (!!playerGO && !!worldGO)
        {
            PlayerController pc = playerGO.GetComponent<PlayerController>();
            World w = worldGO.GetComponent<World>();
            if (!!pc && !!w)
            {
                stored_player_level = pc.level;
                stored_world_level = w.world_state + w.world_substate;
                enableAllCollisionButVoid(defaultState);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check player level
        if (!!playerGO && !!worldGO)
        {
            PlayerController pc = playerGO.GetComponent<PlayerController>();
            World w = worldGO.GetComponent<World>();
            if (!!pc && !!w)
            {
                bool player_level_up = (stored_player_level != pc.level) ;
                bool world_level_up = ( stored_world_level != (w.world_state + w.world_substate) );
                if ( player_level_up || world_level_up ) 
                {
                    if (pc.isSpirit()) // SPIRIT
                        enableAllCollisionButVoid(false);
                    else
                        enableAllCollisionButVoid(true);
                }

                stored_player_level = pc.level;
                stored_world_level = w.world_state + w.world_substate;
            }
        }

        
    }

    public void enableAllCollisionButVoid( bool enable )
    {
        Tilemap[] tms = GetComponentsInChildren<Tilemap>();
        foreach ( Tilemap tm in tms  )
        {
            if (tm.gameObject.name == Constants.TILEMAP_VOID_GO_ID)
                continue;
            else if (tm.gameObject.name == Constants.BROKEN_BRIDGE_WORLD_2_GO_ID)
                continue;
            else
            {
                TilemapCollider2D tc2d = tm.gameObject.GetComponent<TilemapCollider2D>();
                if (!!tc2d)
                    tc2d.enabled = enable;
            }
        }
    }
}
