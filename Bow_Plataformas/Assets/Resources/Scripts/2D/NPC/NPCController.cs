using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public TextAsset dialogos;

    private String currentFrase = "";

    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;

    private GameObject player;
    private BotonInteractuarController botonInteractuarController;

    private List<Frase> frases = new List<Frase>();
    private DialogeController dialogeController;
    public string hablante = "NPC1";

    private bool hablar = false;
    private bool hablando = false;

    private Interactuar2DController interController;
    private PanelDialogoController panelDialogoController;

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        dialogeController = GetComponent<DialogeController>();
        botonInteractuarController = GetComponentInChildren<BotonInteractuarController>();
    }

    void Start()
    {
        cinemachineVirtualCamera = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        
        panelDialogo = GameObject.Find("PanelDialogo");

        panelDialogoController = panelDialogo.GetComponent<PanelDialogoController>();

        frases = dialogeController.getDialogos(dialogos, hablante);
        player = GameObject.Find("Player2D");
    }

    public void inter(GameObject intController)
    {
        if (hablando == false)
        {
            interController = intController.GetComponent<Interactuar2DController>();
            botonInteractuarController.visible();
            hablando = true;
            panelDialogo.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            player.GetComponentInParent<Player2DMovement>().mov = false;
            StartCoroutine("mostrarFrase");
        }
    }

    public void interEnter()
    {
        botonInteractuarController.visible();
    }

    public void interExit()
    {
        botonInteractuarController.visible();
    }

    IEnumerator mostrarFrase()
    {
        bool seguir = true;

        cinemachineVirtualCamera.m_Lens.OrthographicSize = 1f;

        cinemachineVirtualCamera.Follow = transform;

        for (int i = 0; i < frases.Count; i++)
        {
            if (seguir)
            {
                for (int j = 0; j < frases[i].frase.Length; j++)
                {
                    currentFrase = currentFrase + frases[i].frase[j];
                    textoDialogo.text = currentFrase;
                    if (hablando == false)
                    {
                        currentFrase = "";
                        yield break;
                    }

                    yield return new WaitForSeconds(0.01f);
                }

                seguir = false;
                currentFrase = "";
            }

            while (!seguir)
            {
                if (Input.GetButtonDown("Interactuar"))
                {
                    seguir = true;
                }

                yield return null;
            }
        }
        
        cinemachineVirtualCamera.m_Lens.OrthographicSize = 1f;
        cinemachineVirtualCamera.Follow = player.transform;
        panelDialogo.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

        botonInteractuarController.visible();
        dejarHablar();
    }

    private void dejarHablar()
    {
        hablar = false;
        hablando = false;
        panelDialogo.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        player.GetComponentInParent<Player2DMovement>().mov = true;
        player.GetComponentInChildren<Interactuar2DController>().interactuando = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //botonInteractuarController.visible();
            dejarHablar();
        }
    }
}