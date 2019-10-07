using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public bool casting_spell_1 = false;
    public bool casting_spell_2 = false;
    public bool is_teleporting = false;
    public bool is_dashToPlyer = false;

    public bool bossPulled = false;

    // SPELL 1
    int shot_n_missiles = 2;
    public GameObject missile_spell1_GO;
    private List<GameObject> firedProjectiles;

    public const float waitTime = 3f;
    public float elapsedTime = 0f;

    // PLAYER
    private GameObject playerGO;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        bossPulled = false;
        firedProjectiles = new List<GameObject>(shot_n_missiles);
        playerGO = GameObject.Find(Constants.PLAYER_GO_ID);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossPulled)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > waitTime)
                doStuff();

        }//! boss pulled

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
        
        for (int i = 0; i < shot_n_missiles; i++ )
        {
            GameObject newMissile = Instantiate<GameObject>(missile_spell1_GO);
            firedProjectiles.Add(newMissile);
            float deviation_rate = ( 0.1f * i );
            newMissile.transform.position = transform.position;


            
        }


    }

    public void activate()
    {
        bossPulled = true;
    }

    public void clearSpells()
    {
        // clear spell 1
        foreach (GameObject missile in firedProjectiles)
        { Destroy(missile); }
    }

    public void castSpell_2()

    {
        casting_spell_2 = true;
    }

    public void teleport()
    {
        is_teleporting = true;
    }

    public void dashToPlayer()
    {
        is_dashToPlyer = true;
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
