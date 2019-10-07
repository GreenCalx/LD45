using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehavior : MonoBehaviour
{
    public int life = 1;

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    public void Damage(int Dmg)
    {
        life -= Dmg;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
            OnDeath();
        }
    }
}
