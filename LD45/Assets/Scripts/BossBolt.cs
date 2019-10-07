using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBolt : MonoBehaviour
{

    private bool fired = false;
    private Vector2 destination;

    public Vector2 velocity;
    public float smooth_speed = 0.001f;
    public float speed_boost = 1.0f;
    

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
        destination.x = target.position.x + deviation;
        destination.y = target.position.y + deviation;

        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (!!rb2d)
        {
            fired = true;
        }

    }

    private void LateUpdate()
    {
        if (fired)
        {
            Vector2 smoothedPosition = Vector2.SmoothDamp(transform.position, destination, ref velocity, smooth_speed);
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            if (!!rb2d)
            {
                rb2d.velocity = velocity * speed_boost;
            }
        }
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
