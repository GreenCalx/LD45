using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{

    private bool consumed = false;

    // Start is called before the first frame update
    void Start()
    {
        consumed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!!pc && !consumed)
        {
            // player level up 
            pc.levelUp();
            consumed = true;

            // effect ?

            Destroy(this.gameObject);
        
        }
    }
}
