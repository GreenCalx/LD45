using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueTipBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Should not be called, Tongue tip should be a trigger
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we are not a Player we then hit either an ennemy or a wall
        if (!collision.gameObject.GetComponent<PlayerController>())
        {
            // Stop at the hit position
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            // Find Player
            // could probably do GetComponenetInParent too
            GameObject p = GameObject.Find(Constants.PLAYER_GO_ID);
            var pc = p.GetComponent<PlayerController>();
            // Launch either a hit porcedure or a translation
            pc.TongueHit(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().IsAttacking) 
           transform.position = GameObject.Find(Constants.PLAYER_GO_ID).transform.position;
    }
}
