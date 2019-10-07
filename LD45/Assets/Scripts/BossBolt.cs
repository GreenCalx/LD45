using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBolt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireBolt( Transform target, float deviation )
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!!pc)
        {
            pc.hp -= 1;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc =  collision.gameObject.GetComponent<PlayerController>();
        if (!!pc)
        {
            pc.hp -= 1;
        }
        Destroy(this.gameObject);
    }

}
