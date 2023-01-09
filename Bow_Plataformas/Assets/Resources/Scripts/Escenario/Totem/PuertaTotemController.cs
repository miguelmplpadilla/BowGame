using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuertaTotemController : MonoBehaviour
{
    
    private GameObject panelEndGame;

    private void Start()
    {
        panelEndGame = GameObject.Find("PanelFinJuego");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            panelEndGame.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            panelEndGame.transform.Find("TextoFinJuego").GetComponent<TextMeshProUGUI>().text = "You Win";
            Time.timeScale = 0;
        }
    }
}
