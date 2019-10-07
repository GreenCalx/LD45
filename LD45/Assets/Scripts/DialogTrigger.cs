using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public int dialogIndex;
    private GameObject UIGO;

    // Start is called before the first frame update
    void Start()
    {
        UIGO = GameObject.Find(Constants.UI_GO_ID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if ( !!UIGO && !!pc )
        {
            DialogController dc = UIGO.GetComponent<DialogController>();
            if (!!dc)
                dc.startTextLine(dialogIndex);
        }
    }
}
