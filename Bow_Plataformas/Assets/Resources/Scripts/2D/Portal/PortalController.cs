using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    private Animator animator;
    public Animator humo;

    public bool portal;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            humo.SetTrigger("explotar");
        }
    }

    public void setAnimatorEncendido()
    {
        animator.SetTrigger("encender");
    }
}
