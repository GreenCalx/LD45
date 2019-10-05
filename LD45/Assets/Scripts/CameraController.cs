using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RippleEffect))]
public class CameraController : MonoBehaviour
{
    private GameObject playerGO;


    public RippleEffect RE;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find(Constants.PLAYER_GO_ID);

        RE = GetComponent<RippleEffect>();
        RE.enabled = false;
    }

    public void StartRippleEffect()
    {
        RE = GetComponent<RippleEffect>();
        RE.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        // FOLLOW PLAYER
        if (!!playerGO)
        { 
            Transform PlayerTransform = playerGO.transform;
            transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z);
        }
    }

}
