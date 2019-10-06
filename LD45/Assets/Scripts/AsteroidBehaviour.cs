using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public int damage = 1;

    private float elapsed_time = 0f;
    public float waitTime = 6f;

    private Vector3 Velocity = Vector3.zero;
    public float speed = 1f;

    private GameObject playerGO;
    private Vector2 target_pos;

    public GameObject[] random_locations;

    // Start is called before the first frame update
    void Start()
    {
        elapsed_time = 0f;
        playerGO = GameObject.Find(Constants.PLAYER_GO_ID);
        target_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed_time += Time.deltaTime;
        if ( elapsed_time > waitTime )
        {
            float rand = Random.Range(0f, 1f);
            if (rand >= 0.5f)
                dashToPlayer();
            else
                dashToRandom();

            elapsed_time -= waitTime;
        }

    }

    private void LateUpdate()
    {

            //Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            //rb2d.velocity = new Vector2(target_pos.x, target_pos.y) * speed;
            //rb2d.AddForce( new Vector2( target_pos.x, target_pos.y) * speed );

    }

    public void launch()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (!!rb2d)
        {
            rb2d.AddForce(new Vector2(target_pos.x, target_pos.y) * speed);
           // rb2d.velocity = new Vector2(target_pos.x, target_pos.y) * speed;
        }
    }

    public void dashToPlayer()
    {
        if (!!playerGO)
        {
            target_pos = playerGO.transform.position - transform.position;
            launch();
        }
    }

    public void dashToRandom()
    {
        if (random_locations.Length < 3)
            return;

        float rand_res = Random.Range(0f, 3f);
        GameObject random_location;
        if (rand_res < 1f)
            random_location = random_locations[0];
        else if (rand_res < 2f)
            random_location = random_locations[1];
        else
            random_location = random_locations[2];

        if (!!random_location )
        {
            target_pos = random_location.transform.position - transform.position;
            launch();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!!pc)
        {
            pc.hp -= damage;
        }
    }
}
