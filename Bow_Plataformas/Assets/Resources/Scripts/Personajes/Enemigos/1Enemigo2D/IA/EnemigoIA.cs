using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class EnemigoIA : MonoBehaviour
{

    public GameObject player;
    public float speed = 1;
    
    [SerializeReference]
    public Estado FSM;

    private void Awake()
    {
        FSM = new Estado();
    }

    void Start()
    {
        player = GameObject.Find("Player2D");
        
        FSM.setVariables(player, gameObject, speed);
    }

    void Update()
    {
        FSM = FSM.Procesar(); // INICIAMOS LA FSM
    }
}