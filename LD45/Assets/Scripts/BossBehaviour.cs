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
    public float deviation_rate_spell1 = 0.35f;
    int shot_n_missiles = 2;
    public GameObject missile_spell1_GO;
    private List<GameObject> firedProjectiles;

    // SPELL 2
    int shot_n_traps = 1;
    public GameObject crystalsword_GO;
    private List<GameObject> spawned_swords;

    // TELEPORT
    public GameObject[] teleport_locations;

    public float waitTime = 3f;
    public float elapsedTime = 0f;

    // PLAYER
    private GameObject playerGO;
    private GameObject UIGO;

    //POWER
    public int crystals_left = 4;
    private GameObject shieldGO;
    private bool missile_upgrade_consumed = false;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        bossPulled = false;
        missile_upgrade_consumed = false;
        firedProjectiles = new List<GameObject>(shot_n_missiles);
        spawned_swords = new List<GameObject>(shot_n_traps);

        playerGO = GameObject.Find(Constants.PLAYER_GO_ID);
        UIGO = GameObject.Find(Constants.UI_GO_ID);
        shieldGO = GameObject.Find(Constants.BOSS_SHIELD_GO_ID);
        enableShield();
    }

    private void OnDestroy()
    {
        if (!!UIGO)
        {
            DialogController dc = UIGO.GetComponent<DialogController>();
            if (!!dc)
                dc.startBossDeadLine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bossPulled)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > waitTime)
            {
                doStuff();
                elapsedTime -= waitTime;

            }
            updateAnimator();

            checkCrystals();

        }//! boss pulled

        
    }

    public void checkCrystals()
    {
        switch ( crystals_left )
        {
            case 0:
                shieldBreak();
                break;
            case 1:
                waitTime = 2.4f;
                break;
            case 2:
                if (!missile_upgrade_consumed)
                {
                    shot_n_missiles++;
                    firedProjectiles = new List<GameObject>(shot_n_missiles);
                    missile_upgrade_consumed = true;
                }
                break;
            case 3:
                waitTime = 2.7f;
                break;
        }
    }

    public void enableShield()
    {
        CircleCollider2D cc2d = GetComponent<CircleCollider2D>();
        if (!!cc2d)
        {
            cc2d.enabled = false;
        }
    }

    public void shieldBreak()
    {
        CircleCollider2D cc2d = GetComponent<CircleCollider2D>();
        if ( !!shieldGO && !!cc2d )
        {
            cc2d.enabled = true;

            Destroy(shieldGO.gameObject);
        }
    }

    public void doStuff()
    {
        clearSpells();

        // Boss choose what do
        float whatdo = Random.Range(0f, 1f);
        if (whatdo <= 0.25f)
        {
            //dashToPlayer();
            castSpell_2();
        }
        else if (whatdo <= 0.5f)
        {
            castSpell_2();
            //teleport();
        }
        else if (whatdo <= 0.75f)
        {
            castSpell_1();
        }
        else if (whatdo <= 1f)
        {
            castSpell_2();
            teleport();
        }
    }

    public void castSpell_1()
    {
        
        for (int i = 0; i < shot_n_missiles; i++ )
        {
            GameObject newMissile = Instantiate<GameObject>(missile_spell1_GO);
            firedProjectiles.Add(newMissile);
            float deviation = (deviation_rate_spell1 * i );
            newMissile.transform.position = transform.position;
            BossBolt bolt = newMissile.GetComponent<BossBolt>();
            if (!!bolt)
                bolt.fireBolt( playerGO.transform, deviation);
        }

        is_teleporting = false;
        casting_spell_2 = false;
        casting_spell_1 = true;
        is_dashToPlyer = false;

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
        if (!!playerGO)
        {
            for (int i = 0; i < shot_n_traps; i++)
            {
                GameObject newSword = Instantiate<GameObject>(crystalsword_GO);
                spawned_swords.Add(newSword);
                newSword.transform.position = playerGO.transform.position;
            }

            is_teleporting = false;
            casting_spell_2 = true;
            casting_spell_1 = false;
            is_dashToPlyer = false;
        }
    }

    public void teleport()
    {
        if (teleport_locations.Length >= 3)
        {
            float rand = Random.Range(0f, 3f);
            Transform target_loc = transform;

            if (rand <= 1f)
                target_loc = teleport_locations[0].transform;
            else if (rand <= 2f)
                target_loc = teleport_locations[1].transform;
            else if (rand <= 3f)
                target_loc = teleport_locations[2].transform;

            transform.position = target_loc.position;


            is_teleporting = true;
            casting_spell_2 = false;
            casting_spell_1 = false;
            is_dashToPlyer = false;
        }


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
