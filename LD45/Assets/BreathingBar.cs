using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingBar : MonoBehaviour
{
    public float offsetY = 0.5F;
    public float halfSize = 1F;

    public float currentValuePercent = 0;

    LineRenderer LR;
    PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        PC = GameObject.Find(Constants.PLAYER_GO_ID).GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PC.transform.position;
        var BarStartPosition = new Vector2(transform.position.x - halfSize, transform.position.y + offsetY);
        var BarEndPosition = new Vector2(transform.position.x + halfSize, transform.position.y + offsetY);

        var CurrentBarEndPosition = new Vector2( BarStartPosition.x + (currentValuePercent * (halfSize * 2)), BarStartPosition.y);

        Vector3[] Positions = { BarStartPosition, CurrentBarEndPosition };
        LR.SetPositions(Positions);
    }
}
