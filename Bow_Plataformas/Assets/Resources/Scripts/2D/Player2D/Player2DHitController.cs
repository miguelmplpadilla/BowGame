using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DHitController : MonoBehaviour
{

    public float numDamage = 1;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HurtBoxEnemigo"))
        {
            transform.parent.parent = null;
            col.SendMessage("hit", numDamage);
        }
    }
}
