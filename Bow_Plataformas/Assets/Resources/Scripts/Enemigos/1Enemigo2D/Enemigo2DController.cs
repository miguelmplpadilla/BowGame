using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2DController : MonoBehaviour
{
    private GameObject player;

    public bool mov = true;

    public float speed = 2;

    private void Start()
    {
        player = GameObject.Find("Player2D");
    }

    void Update()
    {
        if (mov)
        {
            float distancia = Vector2.Distance(transform.position, player.transform.position);

            if (distancia < 2)
            {
                if (distancia > 0.4f)
                {
                    Vector2 seguirPlayer = new Vector2(player.transform.position.x, transform.position.y);
                    transform.position = Vector2.MoveTowards(transform.position, seguirPlayer, speed * Time.deltaTime);
                }
            }
        }
    }
}
