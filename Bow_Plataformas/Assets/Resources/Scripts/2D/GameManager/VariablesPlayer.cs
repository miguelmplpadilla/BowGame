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

    public int balasAlmacenadas = 9;

    public int numTeseractos;

    public float maxVida = 10;
    public float maxBalas = 3;
    public float maxPuntos = 0;
    public int maxBalasAlmacenadas = 9;

    private RectTransform imagenVida;
    private RectTransform imagenBalas;
    private TextMeshProUGUI textoPuntos;

    public void inicializacion()
    {
        numTeseractos = 0;
            
        vida = maxVida;
        balas = maxBalas;
        puntos = maxPuntos;
        balasAlmacenadas = maxBalasAlmacenadas;

        imagenVida = GameObject.Find("VidaUI").GetComponent<RectTransform>();
        imagenBalas = GameObject.Find("BalasUI").GetComponent<RectTransform>();
        textoPuntos = GameObject.Find("TextoNumBalas").GetComponent<TextMeshProUGUI>();
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
    
    public void sumarBalas(int balasSumar)
    {
        balas += balasSumar;
    }
    
    public void restarBalas(int balasRestar)
    {
        balas -= balasRestar;
    }
    
    public void sumarBalasAlmacenadas(int balasSumar)
    {
        balasAlmacenadas += balasSumar;
    }
    
    public void restarBalasAlmacenadas(int balasRestar)
    {
        balasAlmacenadas -= balasRestar;
    }

    public void actualizarVida()
    {
        imagenVida.sizeDelta = new Vector2(10*vida, imagenVida.sizeDelta.y);
    }
    
    public void actualizarBalas()
    {
        imagenBalas.sizeDelta = new Vector2(9.289f*balas, imagenBalas.sizeDelta.y);
    }

    public void actualizarNumBalas()
    {
        textoPuntos.text = balasAlmacenadas.ToString();
    }
}
