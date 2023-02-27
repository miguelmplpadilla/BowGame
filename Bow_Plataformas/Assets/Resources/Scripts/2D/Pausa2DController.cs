using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Pausa2DController : MonoBehaviour
{
    public GameObject[] canvasDesactivar;

    [SerializeField]
    private bool pausado = false;

    private Canvas selfCanvas;
    private PostProcessVolume mainCamera;
    private ChromaticAberration chromaticAberration;
    private Grain grain;

    private void Awake()
    {
        selfCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<PostProcessVolume>();
        mainCamera.profile.TryGetSettings(out chromaticAberration);
        mainCamera.profile.TryGetSettings(out grain);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            chromaticAberration.active = !pausado;
            grain.active = !pausado;

            selfCanvas.enabled = !pausado;
            
            foreach (var canvas in canvasDesactivar)
            {
                canvas.GetComponent<Canvas>().enabled = pausado;
            }
            
            Time.timeScale = Convert.ToInt32(pausado);
            
            pausado = !pausado;
        }
    }
}
