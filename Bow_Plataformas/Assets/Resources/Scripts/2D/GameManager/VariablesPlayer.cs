using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NuevoJugador", menuName = "Jugador")]
public class VariablesPlayer : ScriptableObject
{
    public float vida = 10;
    public float balas = 3;
    public float puntos = 0;

    public float maxVida = 10;
    public float maxBalas = 3;
    public float maxPuntos = 0;

    private RectTransform imagenVida;
    private RectTransform imagenBalas;
    private TextMeshProUGUI textoPuntos;

    public void inicializacion()
    {
        vida = maxVida;
        balas = maxBalas;
        puntos = maxPuntos;

        imagenVida = GameObject.Find("VidaUI").GetComponent<RectTransform>();
        imagenBalas = GameObject.Find("BalasUI").GetComponent<RectTransform>();
    }

    public void sumarVida(float vidaSumar)
    {
        vida += vidaSumar;
    }
    
    public void restarVida(float vidaRestar)
    {
        vida -= vidaRestar;
    }
    
    public void sumarPuntos(float puntosSumar)
    {
        puntos += puntosSumar;
    }
    
    public void restarPuntos(float puntosRestar)
    {
        puntos -= puntosRestar;
    }
    
    public void sumarBalas(int kunaisSumar)
    {
        balas += kunaisSumar;
    }
    
    public void restarBalas(int kunaisRestar)
    {
        balas -= kunaisRestar;
    }

    public void actualizarVida()
    {
        imagenVida.sizeDelta = new Vector2(10*vida, imagenVida.sizeDelta.y);
    }
    
    public void actualizarBalas()
    {
        imagenBalas.sizeDelta = new Vector2(13*balas, imagenBalas.sizeDelta.y);
    }
}
