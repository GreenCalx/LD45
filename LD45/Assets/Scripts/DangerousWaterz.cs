using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousWaterz : MonoBehaviour
{
    public float KillTimer = 10000F;
    public float currentTime = 0;
    private bool IsRunning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentTime = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsRunning = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            if (!GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().IsTranslating)
                currentTime += Time.deltaTime;
            else
                currentTime = 0;
            if (currentTime > KillTimer)
            {
                GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().dead();
            }
        }
    }
}
