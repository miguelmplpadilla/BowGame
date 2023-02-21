using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class Vigilar : Estado
{
    private GameObject player;
    private GameObject self;
    public Vigilar(GameObject p, GameObject s) : base(p,s)
    {
        player = p;
        self = s;
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

        if (PuedeVerJugador())
        {
            siguienteEstado = new Atacar(player, self);
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

