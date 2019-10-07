using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // INIT GAME IF NEEDED
    }

    // Update is called once per frame
    void Update()
    {
        bool spacePressed = Input.anyKey;
        if (spacePressed)
            SceneManager.LoadScene(Constants.MAIN_GAME_SCENE, LoadSceneMode.Single);

    }


}
