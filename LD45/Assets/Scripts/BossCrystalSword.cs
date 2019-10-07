using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrystalSword : MonoBehaviour
{

    public const float total_duration = 10f;
    public const float waitTime = 3f;
    public float elapsedTime = 0f;

    private bool destroy_me = false;
    private bool trap_triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        destroy_me = false;
        trap_triggered = false;
    }

    // Update is called once per frame
    void Update()
    {

        elapsedTime += Time.deltaTime;
        if (!trap_triggered)
        {
            if (elapsedTime > waitTime)
            { trigger(); elapsedTime -= waitTime; }
        }

        if (elapsedTime > total_duration)
            Destroy(this.gameObject);

    }

    public void trigger()
    {

        Animator animator = GetComponent<Animator>();
        if (!!animator)
        {
            animator.SetBool(Constants.SPELL2_ANIMATOR_TRIGGER_PARM, true);
            trap_triggered = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (!!pc && trap_triggered)
        {
            pc.hp -= 2;
            destroy_me = true;
        }
    }

    private void LateUpdate()
    {
    }

}
