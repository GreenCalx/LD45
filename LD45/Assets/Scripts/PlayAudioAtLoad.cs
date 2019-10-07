using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioAtLoad : MonoBehaviour
{
    GameObject AM;
    public float FameMax = 2000;
    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.Find("Audio Manager");
        var AM_c = AM.GetComponent<AudioManager>();
        AM_c.Play(Constants.SOUND_MAIN_THEME);
    }

    // Update is called once per frame
    void Update()
    {
    }
}