using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUpgrader : MonoBehaviour
{
    // WORLD
    public GameObject worldGO;
    public bool isMajorUpgrade;
    public string SoundFX_OnPïck = "MajorUpgrade";

    // CAMERA
    private GameObject       cameraGO;
    private CameraController cameraController;


    private World world;
    private bool upgradeSpent = false;

    private void activate()
    {
        upgradeSpent = true;

        if (!!worldGO)
        {
            world = worldGO.GetComponent<World>();
            world.upgrade(isMajorUpgrade);
        }

        // Effect
        if (!!cameraController)
            cameraController.StartRippleEffect();

        GameObject.Find("Audio Manager").GetComponent<AudioManager>().Play(SoundFX_OnPïck);

        // Destroy go
        Destroy(this.gameObject);
    }


    /// ------------ UNITY ------------

    // Start is called before the first frame update
    void Start()
    {
        upgradeSpent = false;
        worldGO = GameObject.Find(Constants.WORLD_GO_ID);
        if (!!worldGO)
            world = worldGO.GetComponent<World>();
        cameraGO = GameObject.Find(Constants.CAMERA_GO_ID);
        if (!!cameraGO)
            cameraController = cameraGO.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!!pc && !upgradeSpent) 
            activate();
         
    }
}
