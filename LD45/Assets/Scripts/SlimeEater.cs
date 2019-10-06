using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEater : MonoBehaviour
{

    private bool isChasing = false;
    private GameObject target;
    public float SmoothSpeed = 0.3f;
    private Vector3 Velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (isChasing && !!target)
        {
            Vector3 DesiredPosition;

            DesiredPosition = target.transform.position;
            Vector3 SmoothedPosition = Vector3.SmoothDamp(transform.position, DesiredPosition, ref Velocity, SmoothSpeed);
            Rigidbody2D RB = GetComponent<Rigidbody2D>();
            RB.velocity = Velocity * SmoothSpeed;
        }
        else
        {
            Rigidbody2D RB = GetComponent<Rigidbody2D>();
            RB.velocity = Vector3.zero;
        }
    }

    private void chaseTarget(GameObject iTarget)
    {
        isChasing = true;
        target = iTarget;
    }

    private void stopTarget()
    {
        isChasing = false;
        target = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        CircleCollider2D circleC2D = GetComponent<CircleCollider2D>();  // Detection
        CapsuleCollider2D capsuleC2D = GetComponent<CapsuleCollider2D>();   // ATK
        if (!!pc && !!circleC2D && !!capsuleC2D)
        {
            // Exit if player is just a spirit
            if (pc.level < 2)
                return;

            Collider2D[] circle_overlaps = { }, capsule_overlaps = { };

            // Check in vision range
            circle_overlaps = Physics2D.OverlapCircleAll(circleC2D.transform.position, circleC2D.radius);
            bool triggerdByVision = false;
            foreach (Collider2D c in circle_overlaps)
            {
                if (c == collision)
                { triggerdByVision = true; break; }
            }

            // check in kill range
            capsule_overlaps = Physics2D.OverlapCapsuleAll(capsuleC2D.transform.position, capsuleC2D.size, capsuleC2D.direction, 0);
            bool triggerdByAtkRange = false;
            foreach (Collider2D c in capsule_overlaps)
            {
                if (c == collision)
                { triggerdByAtkRange = true; break; }
            }

            // Resolve
            if (triggerdByVision)
            {
                chaseTarget(collision.gameObject);
                Debug.Log(" IN VISION ");
            }
            if (triggerdByAtkRange)
            {
                pc.hp -= 1;
                Debug.Log(" IN ATK RNGE ");
            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        CircleCollider2D circleC2D = GetComponent<CircleCollider2D>();  // Detection

        if (!!pc && !!circleC2D )
        {
            Collider2D[] circle_overlaps = { };

            // Check in vision range
            circle_overlaps = Physics2D.OverlapCircleAll(circleC2D.transform.position, circleC2D.radius);
            bool outVision = true;
            foreach (Collider2D c in circle_overlaps)
            {
                if (c == collision)
                { outVision = false; break; }
            }

            // Resolve
            if (outVision)
            {
                stopTarget();
                Debug.Log(" OUT VISION ");
            }

        }
    }
}
