using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ManchaSangreController : MonoBehaviour
{
    public Sprite[] sprites;
    
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Length)];
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down,10, 1 << 15);

        Debug.DrawRay(transform.position, Vector2.down, Color.red);

        transform.position = hitInfo.point + new Vector2(0,0.001f);
    }
}
