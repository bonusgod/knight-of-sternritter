using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        lives = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }
}
