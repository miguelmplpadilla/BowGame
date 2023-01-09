using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TotemController : MonoBehaviour
{
    public Color[] colores;

    public GameObject posicionesCuboDestruir;

    private Transform[] posicionesCubos;
    
    public GameObject cuboDestruir;

    private Renderer renderer;

    private List<GameObject> cubosADestruir = new List<GameObject>();

    public TextMeshProUGUI textoCantidadCubos;
    private SceneController sceneController;

    public Animator[] puertasAnimator;

    private bool endGame = false;

    private GameObject panelEndGame;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = colores[Random.Range(0, colores.Length)];
    }

    private void Start()
    {
        panelEndGame = GameObject.Find("PanelFinJuego");
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        posicionesCubos = posicionesCuboDestruir.GetComponentsInChildren<Transform>();
        
        desordenarArray(posicionesCubos);

        for (int i = 0; i < Random.Range(6, posicionesCubos.Length); i++)
        {
            GameObject prefabCuboDestruir = Instantiate(cuboDestruir);
            cubosADestruir.Add(prefabCuboDestruir);
            prefabCuboDestruir.transform.position = posicionesCubos[i].position;

            Renderer[] renderersCuboDestruir = prefabCuboDestruir.transform.GetComponentsInChildren<Renderer>();

            for (int j = 0; j < renderersCuboDestruir.Length; j++)
            {
                renderersCuboDestruir[j].material.color = renderer.material.color;
            }
        }

        sceneController.iniciarContador = true;
    }

    private void LateUpdate()
    {
        int cont = 0;
        
        for (int i = 0; i < cubosADestruir.Count; i++)
        {
            if (cubosADestruir[i] == null || cubosADestruir[i].GetComponent<CuboDestruirController>().destruido)
            {
                cont++;
            }
        }

        textoCantidadCubos.text = ((cubosADestruir.Count - 1) - cont).ToString();

        if (((cubosADestruir.Count - 1) - cont) == 0 && sceneController.totalSegundos > 0 && !endGame)
        {
            for (int i = 0; i < puertasAnimator.Length; i++)
            {
                puertasAnimator[i].SetBool("abrir", true);
            }

            endGame = true;
            sceneController.iniciarContador = false;
        }

        if (sceneController.totalSegundos <= 0 && !endGame)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            panelEndGame.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            panelEndGame.transform.Find("TextoFinJuego").GetComponent<TextMeshProUGUI>().text = "Game Over";
            Time.timeScale = 0;
            endGame = true;
            sceneController.iniciarContador = false;
        }
    }

    public void desordenarArray(Transform[] listaDesordenar)
    {
        int n = listaDesordenar.Length;

        for (int i = n - 1; i < 0; i--)
        {
            int j = Random.Range(0, i);
            Transform temp = listaDesordenar[i];
            listaDesordenar[i] = listaDesordenar[j];
            listaDesordenar[j] = temp;
        }
    }
}
