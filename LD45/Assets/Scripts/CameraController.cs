using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RippleEffect))]
public class CameraController : MonoBehaviour
{

    public RippleEffect RE;
    // Start is called before the first frame update
    void Start()
    {
        RE = GetComponent<RippleEffect>();
        RE.enabled = false;
    }

    public void StartRippleEffect()
    {
        RE.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
