﻿using System.Collections;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsRunning = false;
        currentTime = 0;
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
            GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().IsOnWater = true;
            if (!GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().IsTranslating
                && !GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().IsAttacking)
                currentTime += Time.deltaTime;
            else
            {
                currentTime = 0;
                IsRunning = false;
            }
            if (currentTime > KillTimer)
            {
                GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().dead();
            }
        }
        else
        {
            GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().IsOnWater = false;
        }
    }
}
