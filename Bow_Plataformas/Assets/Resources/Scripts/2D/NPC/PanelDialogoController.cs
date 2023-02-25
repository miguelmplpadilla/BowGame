using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDialogoController : MonoBehaviour
{

    private Image imagenNPC;
    
    [Serializable]
    public class ImagenPlanel
    {
        public string id;
        public Sprite imagenPanel;
    }

    
    public List<ImagenPlanel> listaImagenesPanel = new List<ImagenPlanel>();

    public IDictionary<string, Sprite> imagenesPanel = new Dictionary<string, Sprite>();


    private void Awake()
    {
        foreach (var iPanel in listaImagenesPanel)
        {
            imagenesPanel.Add(iPanel.id, iPanel.imagenPanel);
        }
    }

    private void Start()
    {
        imagenNPC = GameObject.Find("ImagenNPC").GetComponent<Image>();
    }

    public void setImagenPanel(string hablante)
    {
        imagenNPC.sprite = imagenesPanel[hablante];
    }
}
