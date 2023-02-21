using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Atacar : Estado
{
    private GameObject player;
    private GameObject self;
    
    public Atacar(GameObject p, GameObject s) : base(p,s)
    {
        Debug.Log("ATACAR");
        player = p;
        self = s;
        nombre = ESTADO.ATACAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entrar();
    }

    public override void Actualizar()
    {

        if (!PuedeAtacar())
        {
            siguienteEstado = new Vigilar(player, self); // Si el NPC no puede atacar al jugador, lo ponemos a vigilar (por ejemplo).
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de ATACAR a VIGILAR.
        }
    }

    public override void Salir()
    {
        // Le resetearíamos la animación de disparar, o lo que sea...
        base.Salir();
    }

    public bool PuedeAtacar()
    {
        return Vector2.Distance(self.transform.position, player.transform.position) > 0.4f;
    }
}