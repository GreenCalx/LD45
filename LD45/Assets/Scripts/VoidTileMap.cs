using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VoidTileMap : MonoBehaviour
{
    private GameObject playerGO;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find(Constants.PLAYER_GO_ID);
    }

    // Update is called once per frame
    void Update()
    {
        if (!!playerGO)
        {
            PlayerController pc = playerGO.GetComponent<PlayerController>();
            TilemapCollider2D tc2d = GetComponent<TilemapCollider2D>();
            if (!!pc && !!tc2d)
                tc2d.enabled = pc.acquired_void_collision ;
        }
    }
}
