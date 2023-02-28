using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class Vigilar : Patrulla
{
    public Vigilar() : base()
    {
        Debug.Log("VIGILAR");
        nombre = ESTADO.VIGILAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        base.Entrar();
    }

    public override void Actualizar()
    {
        // Le decimos que se vaya moviendo y patrullando...

        Debug.Log("Vigilando");

        if (PuedeVerJugador())
        {
            siguienteEstado = new Atacar();
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de VIGILAR a ATACAR.
        }
    }

    public override void Salir()
    {
        base.Salir();
    }

    // Puede el NPC ver el jugador?
    public bool PuedeVerJugador()
    {
       return  Vector2.Distance(self.transform.position, player.transform.position) < 2;
    }
}

