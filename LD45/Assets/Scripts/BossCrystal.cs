using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrystal : MonoBehaviour
{
    public int life = 1;
    private GameObject bossGO;

    public void OnDeath()
    {
        if (!!bossGO)
        {
            BossBehaviour bb = bossGO.GetComponent<BossBehaviour>();
            bb.crystals_left -= 1;
        }

        Destroy(gameObject);
    }

    public void Damage(int Dmg)
    {
        life -= Dmg;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossGO = GameObject.Find(Constants.BOSS_GO_ID);
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            OnDeath();
        }
    }

}
