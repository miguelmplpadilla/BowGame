using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumoController : MonoBehaviour
{
    private Animator animator;
    public Animator portal;

    public GameObject NPCFinal;

    private bool encendido = false;

    private bool npcInstanciado = false;

    private GameObject npcFinalInstanciado;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void setAnimatorEncendido()
    {
        portal.SetBool("encendido", !encendido);
        encendido = !encendido;

        if (!npcInstanciado)
        {
            npcFinalInstanciado = Instantiate(NPCFinal, transform.parent.position, Quaternion.identity);
            npcInstanciado = true;
        }
        else
        {
            Destroy(npcFinalInstanciado);
        }
    }
}
