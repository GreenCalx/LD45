using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public bool casting_spell_1 = false;
    public bool casting_spell_2 = false;

    public const float waitTime = 3f;
    public float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > waitTime)
            doStuff();

        // animator
        updateAnimator();
    }

    public void doStuff()
    {
        // Boss choose what do
        float whatdo = Random.Range(0f, 1f);
        if (whatdo <= 0.25f)
        {
            dashToPlayer();
        }
        else if (whatdo <= 0.5f)
        {
            teleport();
        }
        else if (whatdo <= 0.75f)
        {
            castSpell_1();
        }
        else if (whatdo <= 1f)
        {
            castSpell_2();
        }
    }

    public void castSpell_1()
    {
        casting_spell_1 = true;
    }

    public void castSpell_2()
    {
        casting_spell_2 = true;
    }

    public void teleport()
    {

    }

    public void dashToPlayer()
    {
        
    }

    public void updateAnimator()
    {
        Animator animator = GetComponent<Animator>();
        if (!!animator)
        {
            animator.SetBool( Constants.BOSS_ANIMATOR_CAST1_PARM, casting_spell_1 );
            animator.SetBool( Constants.BOSS_ANIMATOR_CAST2_PARM, casting_spell_2 );
        }
    }
}
