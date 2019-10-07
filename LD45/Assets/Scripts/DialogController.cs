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
        "This essence builds your universe.\nMany more can be collected.", // done
        "You found some lifeforce ! \n You are a Spirit now.\nOnly the deep void can stop you!", // done
        
        // WORLD 2
        "The next lifeforce lies at the heart of the labyrinthe. \nDon't forget to collect essences.", // done
        "You found some lifeforce ! \nYou are a Slime now.\nJust a weak physical creature with a physical body.", // done
        "The next lifeforce lies across the broken bridge. \nDon't forget to collect essences.",  // done

        // WORLD 3
        "You found some lifeforce ! \nYou are a Tongueling now.\nUse your tongue with SHIFT to lick around.", // done

        // WORLD 4
        "You dried the lake!\nYou can now walk back to the labyrinth and claim this universe !", // done
        "You found some lifeforce ! \nYou fully recovered !\nUse LEFT ALT to smite your foes.", // done
        "You killed Zombologg !\nYou successfully claimed this universe !"
    };

    public  float  dialog_min_duration  = 2.0f;
    private float  timer                = 0f;

    private Dictionary<int, bool> lines_consumed;
    
    public void startExitenceLine()
    { startTextLine(0); }
    public void startControlLine()
    { startTextLine(1); }
    public void startFirstMinorEssenceLine()
    { startTextLine(2); }
    public void startSpiritLine()
    { startTextLine(3); }
    public void startW2LabHint()
    { startTextLine(4); }
    public void startSlimeLine()
    { startTextLine(5); }
    public void startBrokenBridge2HintLine()
    { startTextLine(6); }
    public void startTonguelingLine()
    { startTextLine(7); }
    public void startDriedLakeLine()
    { startTextLine(8); }
    public void startFullyRecoveredLine()
    { startTextLine(9); }
    public void startBossDeadLine()
    { startTextLine(10); }

    public void startPlayerFormText( int iFormLevel )
    {
        if (iFormLevel == 1)
            startSpiritLine();
        else if (iFormLevel == 2)
            startSlimeLine();
        else if (iFormLevel == 3)
            startTonguelingLine();
        else if (iFormLevel == 4)
            startFullyRecoveredLine();
    }


    public void startTextLine(int index)
    {
        Text t = GetComponentInChildren<Text>();
        if (!!t && !lines_consumed[index])
        {
            t.enabled = true;
            dialog_is_on = true;
            t.text = lines[index];
            lines_consumed[index] = true;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        lines_consumed = new Dictionary<int,bool>();
        for (int i=0; i< lines.Length;i++)
        {
            lines_consumed.Add(i, false);
        }

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
