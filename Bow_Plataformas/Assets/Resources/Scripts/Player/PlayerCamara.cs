using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Random = UnityEngine.Random;

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

    public float shakeDuration = 0;
    public float shakeAmount = 2;
    public float decreaseFactor = 1;
    private Vector3 originalPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        combateController = GetComponentInParent<CombateController>();

        originalPos = transform.position;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        shake();
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
        if (shakeDuration <= 0)
        {
            mouseX += Input.GetAxis("Mouse X") * lookSensitivity;
            mouseY += Input.GetAxis("Mouse Y") * lookSensitivity;
            mouseY = Mathf.Clamp(mouseY, -30, 90);

            transform.LookAt(target);

            player.rotation = Quaternion.Euler(0, mouseX, 0);

            target.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        }
    }

    public void shake()
    {
        if (shakeDuration > 0)
        {
            if (animator.enabled)
            {
                originalPos = transform.localPosition;
                animator.enabled = false;
                transform.localPosition = originalPos;
            }
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            if (!animator.enabled)
            {
                animator.enabled = true;
            }
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }
}
