using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesbloquearMapaController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 lastPosition;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);

        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (lastPosition != transform.position)
        {
            lineRenderer.positionCount++;
            int positionIndex = lineRenderer.positionCount - 1;
        
            lineRenderer.SetPosition(positionIndex, transform.position);

            lastPosition = transform.position;
        }
    }
}
