using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogController : MonoBehaviour
{
    private bool dialog_is_on = false;
    private static readonly string[] lines =
    {
        "Hello there, newborn.\nTake CONTROL and check that shiny square out !",
        "You gained control !\nUse WASD to move around."
    };

    public  float  dialog_min_duration  = 2.0f;
    private float  timer                = 0f;
   
    
    public void startExitenceLine()
    {
        
        Text t = GetComponentInChildren<Text>();
        if (!!t)
        {
            t.enabled = true;
            dialog_is_on = true;
            t.text = lines[0];
        }
    }

    public void startControlLine()
    {
        Text t = GetComponentInChildren<Text>();
        if (!!t)
        {
            t.enabled = true;
            dialog_is_on = true;
            t.text = lines[1];
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        dialog_is_on = false;
        Text t = GetComponentInChildren<Text>();
        if (!!t)
        {
            t.enabled = false;
        }
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialog_is_on)
        {
            timer += Time.deltaTime;
            if ( Input.anyKey && ( timer > dialog_min_duration) )
            {
                Text t = GetComponentInChildren<Text>();
                if (!!t)
                {
                    t.enabled = false;
                    dialog_is_on = false;

                    timer = timer - dialog_min_duration;
                }
            }
        }
    }
}
