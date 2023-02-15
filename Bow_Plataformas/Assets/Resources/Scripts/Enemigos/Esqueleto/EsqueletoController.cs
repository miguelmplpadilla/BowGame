using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EsqueletoController : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private HurtEnemyController hurtEnemyController;

    public bool seguirPlayer = true;

    public bool atacando = false;

    void Start()
    {
        player = GameObject.Find("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hurtEnemyController = GetComponentInChildren<HurtEnemyController>();
    }

    
    void Update()
    {
        float distancia = Vector3.Distance(player.transform.position, transform.position);

        if (!hurtEnemyController.muerto && seguirPlayer)
        {
            if (distancia > 0.6f)
            {
                animator.SetBool("run", true);
                navMeshAgent.SetDestination(player.transform.position);
            }
            else
            {
                animator.SetBool("run", false);
                if (!atacando)
                {
                    StartCoroutine("atacar");
                    atacando = true;
                }
            }
        }
    }

    IEnumerator atacar()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("atacar");
        StartCoroutine("setAtacandoFalse");
        yield return null;
    }

    IEnumerator setAtacandoFalse()
    {
        yield return new WaitForSeconds(2f);
        atacando = false;
        yield return null;
    }

    public void takeHit()
    {
        animator.SetTrigger("hit");
        atacando = false;
        StopCoroutine("atacar");
    }
}
