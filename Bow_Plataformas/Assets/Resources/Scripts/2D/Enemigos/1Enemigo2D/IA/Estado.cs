using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estado
{
    protected GameObject player;
    protected GameObject self;
    protected float speed;

    // 'ESTADOS' que tiene el NPC
    public enum ESTADO
    {
        VIGILAR, ATACAR, PATRULLA
    };

    // 'EVENTOS' - En que parte nos encontramos del estado
    public enum EVENTO
    {
        ENTRAR, ACTUALIZAR, SALIR
    };

    public ESTADO nombre; // Para guardar el nombre del estado
    protected EVENTO faseActual; // Para guardar la fase en la que nos encontramos
    protected Estado siguienteEstado; // El estado que se EJECUTAR A CONTINUACI�N del estado actual

    public void setVariables(GameObject p, GameObject s, float velocidad)
    {
        player = p;
        self = s;
        speed = velocidad;
    }

    public Estado()
    {
    }

    // Las fases de cada estado
    public virtual void Entrar()
    {
        faseActual = EVENTO.ACTUALIZAR;
    } // La primera fase que se ejecuta cuando cambiamos de estado. El siguiente estado deberia ser "actualizar".
    public virtual void Actualizar() { faseActual = EVENTO.ACTUALIZAR;} // Una vez estas en ACTUALIZAR, te quedas en ACTUALIZAR hasta que quieras cambiar de estado.
    public virtual void Salir() { faseActual = EVENTO.SALIR; } // La fase de SALIR es la ltima antes de cambiar de ESTADO, aqui deberiamos limpiar lo que haga falta.

    // Este es la funcion a la que llamaremos para que el NPC inicie la maquina de estados. Vincula los EVENTOS con las funciones que ejecuta cada uno
    public Estado Procesar()
    {
        if (faseActual == EVENTO.ENTRAR) Entrar();
        if (faseActual == EVENTO.ACTUALIZAR) Actualizar();
        if (faseActual == EVENTO.SALIR)
        {
            Salir();
            return siguienteEstado; // IMPORTANTE: Aqui hacemos el cambio de estado.
        }
        return this; // Si no salimos por el return de arriba, seguimos en el mismo estado.
    }

}

