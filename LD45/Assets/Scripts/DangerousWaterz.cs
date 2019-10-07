using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousWaterz : MonoBehaviour
{
    public float KillTimer = 10000F;
    public float currentTime = 0;
    public bool IsRunning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            currentTime = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            IsRunning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            IsRunning = false;
            currentTime = 0;
        }
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
                GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().WaterDead();
            }

            var BreathingBar=  GameObject.Find(Constants.BREATHING_BAR_ID);
            var BreathingBarScript = BreathingBar.GetComponent<BreathingBar>();
            var TimeAsPercentInverse = 1 - (currentTime / KillTimer);
            BreathingBarScript.currentValuePercent = TimeAsPercentInverse;
        }
        else
        {
            
            GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>().IsOnWater = false;
        }
    }
}
