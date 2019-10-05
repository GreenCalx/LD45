using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SubformTile : MonoBehaviour
{
    // Level of subform needed to see activate this layer of tile
    public int subform_depth = 1;
    public int fade_in_step = 1;

    private GameObject worldGO;
    private float alpha = 0f;

    // Start is called before the first frame update
    void Start()
    {
        worldGO = GameObject.Find(Constants.WORLD_GO_ID);
        alpha = 0.0f;
        
        Tilemap t = GetComponent<Tilemap>();
       if (!!t)
            t.color = new Color( 255, 255, 255, alpha);
            
    }
        
    // Update is called once per frame
    void Update()
    {
        if (!!worldGO && alpha < 1f)
        {
            World w = worldGO.GetComponent<World>();
            Tilemap t = GetComponent<Tilemap>();
            if (!!w && !!t)
            {
                if (w.world_substate >= subform_depth)
                {
                    alpha = t.color.a + fade_in_step;
                    t.color = new Color(255, 255, 255, alpha);
                }
            }
        }
    }
}
