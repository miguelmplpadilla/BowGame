using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Vector3 movement;

    public float minSpeed = 1;
    public float maxSpeed = 1;
    
    private float speed = 1;

    private Animator animator;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        movimiento();
    }

    private void movimiento()
    {
        float axisVertical = Input.GetAxisRaw("Vertical");
        float axisHorizontal = Input.GetAxisRaw("Horizontal");

        Debug.Log(axisVertical);
        
        if (axisVertical != 0 || axisHorizontal != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 1.5f;
            }
            else
            {
                speed = 1;
            }
            
            animator.SetBool("run", true);
            animator.SetFloat("velocidad", speed);
        }
        else
        {
            animator.SetBool("run", false);
        }
        
        movement = new Vector3(axisHorizontal, 0,axisVertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }
}
