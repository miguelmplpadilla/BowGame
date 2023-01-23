using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigoController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public Camera camera;

    private GameObject player;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(rayo, out hit, 100))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }*/

        Vector3 posicionSeguir = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        navMeshAgent.SetDestination(posicionSeguir);
    }
}
