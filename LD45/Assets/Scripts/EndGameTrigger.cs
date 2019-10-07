using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!pc)
            pc = collision.GetComponentInParent<PlayerController>();
        GameObject bossGO = GameObject.Find(Constants.BOSS_GO_ID);
        bool boss_alive = (bossGO != null);
        if (!!pc && !boss_alive)
        {
            bool spacePressed = Input.GetKey(KeyCode.Space);
            if (spacePressed)
                SceneManager.LoadScene(Constants.END_GAME_SCENE, LoadSceneMode.Single);

        }
    }
}
