using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererController : MonoBehaviour
{
    public bool visible = true;

    private Camera camara;

    private void Start()
    {
        camara = GameObject.Find("Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        visible = isVisibleFrom(GetComponent<Renderer>(), camara);
    }
    
    public static bool isVisibleFrom(Renderer renderer, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
