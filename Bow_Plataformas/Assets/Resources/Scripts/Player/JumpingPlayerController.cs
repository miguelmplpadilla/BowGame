using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlayerController : MonoBehaviour
{

    private bool saltar = false;
    private GameObject jumpingBlock;
    private GameObject player;
    
    private Animator animator;
    private PlayerController playerController;

    private bool dejarSaltar = false;

    private void Awake()
    {
        player = transform.parent.gameObject;
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (saltar)
        {
            if (Input.GetButtonDown("Jump"))
            {
                dejarSaltar = false;
                playerController.mov = false;
                playerController.saltando = true;
                animator.SetBool("run", false);
                animator.SetTrigger("jumpBlock");
                StartCoroutine("saltando");
            }
        }
    }

    IEnumerator saltando()
    {
        jumpingBlock.GetComponentInParent<BoxCollider>().enabled = false;
        
        player.transform.rotation = Quaternion.Euler(jumpingBlock.transform.forward.x,jumpingBlock.transform.forward.y,
            jumpingBlock.transform.forward.z);

        StartCoroutine("pararSalto");
        
        while (true)
        {
            Vector3 movement = new Vector3(player.transform.forward.x,player.transform.forward.y,
                player.transform.forward.z) * 0.5f * Time.deltaTime;
            player.transform.Translate(movement, Space.Self);

            if (dejarSaltar)
            {
                break;
            }
            
            yield return null;
        }
        
        jumpingBlock.GetComponentInParent<BoxCollider>().enabled = true;
        
        playerController.mov = true;
        playerController.saltando = false;
        
        yield return null;
    }

    IEnumerator pararSalto()
    {
        yield return new WaitForSeconds(1f);
        dejarSaltar = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpingBlock"))
        {
            jumpingBlock = other.gameObject;
            saltar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JumpingBlock"))
        {
            saltar = false;
        }
    }
}
