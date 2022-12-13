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
    private bool empezarMoverse = false;

    private void Awake()
    {
        player = transform.parent.gameObject;
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        /*if (saltar)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpingBlock.GetComponentInParent<BoxCollider>().enabled = false;
                
                dejarSaltar = false;
                animator.SetBool("jumpBlock", true);
                
                playerController.mov = false;
                playerController.saltando = true;

                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 
                    jumpingBlock.transform.position.z);

                Vector3 rotacionPlayer;

                if (player.transform.position.z > jumpingBlock.transform.position.z)
                {
                    rotacionPlayer = new Vector3(jumpingBlock.transform.forward.x, -jumpingBlock.transform.forward.y,
                        jumpingBlock.transform.forward.z);
                }
                else
                {
                    rotacionPlayer = new Vector3(jumpingBlock.transform.forward.x, jumpingBlock.transform.forward.y,
                        jumpingBlock.transform.forward.z);
                }
                
                player.transform.rotation = Quaternion.Euler(rotacionPlayer.x,rotacionPlayer.y,
                    rotacionPlayer.z);
            }
        }*/
    }

    IEnumerator saltando()
    {
        while (true)
        {
            Vector3 movement = new Vector3(player.transform.forward.x,player.transform.forward.y,
                player.transform.forward.z) * 1f * Time.deltaTime;
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

    public void startSaltando()
    {
        StartCoroutine("saltando");
    }

    public void setDejarSaltarTrue()
    {
        dejarSaltar = true;
    }
}
