using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamara : MonoBehaviour
{
    public float lookSensitivity = 1;
    public Transform target;
    public Transform player;

    private float mouseX;
    public float mouseY;

    private PlayerController playerController;
    private CombateController combateController;

    private Animator animator;

    public Vector3 direccionDisparo;
    public RaycastHit hitInfo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        combateController = GetComponentInParent<CombateController>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!playerController.saltando && !combateController.fijado)
        {
            controlCamera();
        }
    }

    private void controlCamera()
    {
        mouseX += Input.GetAxis("Mouse X") * lookSensitivity;
        mouseY += Input.GetAxis("Mouse Y") * lookSensitivity;
        mouseY = Mathf.Clamp(mouseY, -30, 90);
        
        transform.LookAt(target);
        
        player.rotation = Quaternion.Euler(0, mouseX, 0);
        
        target.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }
}
