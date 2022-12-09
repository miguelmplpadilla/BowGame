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
        float axisHorizontal = Input.GetAxisRaw("Vertical");
        
        movement = new Vector3(axisHorizontal, 0,axisVertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        if (axisVertical != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 4;
            }
            else
            {
                speed = 2;
            }
            
            animator.SetBool("run", true);
            animator.SetFloat("velocity", speed);
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
