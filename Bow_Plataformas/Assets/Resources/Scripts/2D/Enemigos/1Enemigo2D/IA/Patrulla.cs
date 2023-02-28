using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrulla : Estado
{
    public Patrulla() : base()
    {
        Debug.Log("PATRULLA");
        nombre = ESTADO.PATRULLA; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        base.Entrar();
    }

    public override void Actualizar()
    {
        siguienteEstado = new Vigilar();
        faseActual = EVENTO.SALIR;
    }

    public override void Salir()
    {
        base.Salir();
    }
}
