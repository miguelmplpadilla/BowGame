using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Vector3 movement;
    public float speed;

    private Animator animator;

    private GameObject camara;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camara = GameObject.Find("Camara");
    }

    
    void Update()
    {
        movimiento();
    }

    private void movimiento()
    {
        float axisVertical = Input.GetAxisRaw("Vertical");

        Debug.Log(camara.transform.forward.z);
        
        movement = new Vector3(0, 0,axisVertical * camara.transform.forward.z);

        if (axisVertical > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    private void FixedUpdate()
    {
        float verticalVelocity = movement.normalized.z * speed;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, verticalVelocity);
    }
}
