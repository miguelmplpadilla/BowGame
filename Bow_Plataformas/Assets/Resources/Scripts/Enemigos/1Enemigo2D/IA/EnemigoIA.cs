using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class EnemigoIA : MonoBehaviour
{

    public GameObject player;
    public float distancia = 0;
    
    public Estado FSM;

    void Start()
    {
        player = GameObject.Find("Player2D");
        FSM = new Vigilar(player, gameObject); // CREAMOS EL ESTADO INICIAL DEL NPC
    }

    void Update()
    {
        distancia = Vector2.Distance(player.transform.position, transform.position);
        FSM = FSM.Procesar(); // INICIAMOS LA FSM
    }
}