using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DGroundController : MonoBehaviour
{
    public bool isGrounded = false;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
