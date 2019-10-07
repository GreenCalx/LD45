using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogController : MonoBehaviour
{
    private bool dialog_is_on = false;
    private static readonly string[] lines =
    {
        // WORLD 0
        "Hello there, newborn.\nTake CONTROL and check that primary essence out !", // done
        "You gained control !\nUse WASD/Arrows to move around.", // done
        
        // WORLD 1
        "This essence builds your universe.\nMany more can be collected.",
        "You found some lifeforce ! \n You are a Spirit now.\nOnly the deep void can stop you!",
        
        // WORLD 2
        "The next lifeforce lies at the heart of the labyrinthe. \nDon't forget to collect essences.",
        "You found some lifeforce ! \nYou are a Slime now.\nJust a weak physical creature with a physical body.",
        "The next lifeforce lies across the broken bridge. \nDon't forget to collect essences.",

        // WORLD 3
        "You found some lifeforce ! \nYou are a Tongueling now.\nUse your tongue with SHIFT to lick around.",
        "You dried the lake!\nYou can now walk back to the labyrinth and claim this universe !",

        // WORLD 4
        "You found some lifeforce ! \nYou fully recovered !\nUse LEFT ALT to smite your foes.",
        "You killed Zombologg !\nYou successfully claimed this universe !"
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
