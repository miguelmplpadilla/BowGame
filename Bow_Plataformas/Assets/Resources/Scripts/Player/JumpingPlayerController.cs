using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlayerController : MonoBehaviour
{
    public bool saltar = true;
    private GameObject jumpingBlock;
    private GameObject player;
    
    private Animator animator;
    private PlayerController playerController;
    private Rigidbody rigidbody;

    private bool dejarSaltar = false;
    private bool empezarMoverse = false;

    public float jumpingForce = 1f;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponentInParent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
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

        if (saltar)
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("jump");
                rigidbody.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
                saltar = false;
            }
        }
    }

    /*IEnumerator saltando()
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
    }*/
}
