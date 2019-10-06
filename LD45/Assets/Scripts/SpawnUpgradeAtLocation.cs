using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUpgradeAtLocation : MonoBehaviour
{
    public GameObject location;
    public GameObject world_upgrade;

    private bool hasBeenTriggered = false;

    public void trigger()
    {
        if (!hasBeenTriggered)
        {
            GameObject newUpgradeGO = Instantiate(world_upgrade);
            newUpgradeGO.transform.position = location.transform.position;
            hasBeenTriggered = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hasBeenTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!!pc)
        {
            trigger();
        }
    }
}
