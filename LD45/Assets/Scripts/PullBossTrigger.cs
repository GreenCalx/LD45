using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBossTrigger : MonoBehaviour
{

    private GameObject bossGO;

    // Start is called before the first frame update
    void Start()
    {
        bossGO = GameObject.Find(Constants.BOSS_GO_ID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!!pc &&!!bossGO)
        {
            BossBehaviour bb = bossGO.GetComponent<BossBehaviour>();
            if (!!bb)
                bb.bossPulled = true;
        }
    }
}
