using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Atacar : Estado
{
    
    public Atacar()
    {
        Debug.Log("ATACAR");
        nombre = ESTADO.ATACAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entrar();
    }

    public override void Actualizar()
    {
        Debug.Log("Atacando");
        if (!PuedeAtacar())
        {
            if (!PuedeVerJugador())
            {
                siguienteEstado = new Vigilar(); // Si el NPC no puede atacar al jugador, lo ponemos a vigilar (por ejemplo).
                faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de ATACAR a VIGILAR.
            }
            
            Vector2 seguirPlayer = new Vector2(player.transform.position.x, self.transform.position.y);
            self.transform.position = Vector2.MoveTowards(self.transform.position, seguirPlayer, speed * Time.deltaTime);
        }
        else
        {
            
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

    public bool PuedeVerJugador()
    {
        return  Vector2.Distance(self.transform.position, player.transform.position) < 2;
    }
}