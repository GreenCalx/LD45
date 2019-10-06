using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpiritNoCollidHandler : MonoBehaviour
{
    public bool defaultState = true;

    GameObject playerGO;
    private int stored_player_level;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find(Constants.PLAYER_GO_ID);
        if (!!playerGO)
        {
            PlayerController pc = playerGO.GetComponent<PlayerController>();
            if (!!pc)
            {
                stored_player_level = pc.level;
                enableAllCollisionButVoid(defaultState);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check player level
        if (!!playerGO)
        {
            PlayerController pc = playerGO.GetComponent<PlayerController>();
            if (!!pc)
            {
                if ( stored_player_level != pc.level) // PLAYER LEVELD UP
                {
                    if (pc.level < 2) // SPIRIT
                        enableAllCollisionButVoid(false);
                    else
                        enableAllCollisionButVoid(true);
                }

                stored_player_level = pc.level;

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
