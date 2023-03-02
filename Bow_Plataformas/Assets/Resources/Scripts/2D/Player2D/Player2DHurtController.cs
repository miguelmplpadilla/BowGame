using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player2DHurtController : MonoBehaviour
{
    public VariablesPlayer variablesPlayer;

    private Player2DMovement player2DMovement;
    private Player2DAtack player2DAtack;
    private Animator animator;
    private Rigidbody2D rigidbody;

    public bool golpeado = false;

    private CinemachineVirtualCamera CM;
    private PostProcessVolume mainCamera;
    private ChromaticAberration chromaticAberration;
    private Grain grain;

    public GameObject ojos;

    private Canvas canvasMuerte;

    private void Awake()
    {
        player2DMovement = GetComponentInParent<Player2DMovement>();
        player2DAtack = GetComponentInParent<Player2DAtack>();
        animator = GetComponentInParent<Animator>();
        rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    private void Start()
    {
        CM = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        
        mainCamera = GameObject.Find("MainCamera").GetComponent<PostProcessVolume>();
        mainCamera.profile.TryGetSettings(out chromaticAberration);
        mainCamera.profile.TryGetSettings(out grain);

        canvasMuerte = GameObject.Find("CanvasMuerte").GetComponent<Canvas>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HitBoxEnemigo"))
        {
            if (!golpeado)
            {
                hit(col.GetComponent<DamageController>().damage, col.gameObject);
            }
        }
    }

    public void hit(float damage, GameObject collider)
    {
        if (variablesPlayer.vida > 0)
        {
            rigidbody.velocity = Vector2.zero;
            player2DMovement.mov = true;
            player2DMovement.golpeado = true;
            player2DAtack.atacando = false;
            player2DAtack.shoot = false;
            golpeado = true;
                
            animator.SetTrigger("hit");
            
            variablesPlayer.restarVida(damage);

            if (collider != null)
            {
                if (collider.GetComponent<DamageController>().destruir)
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if (variablesPlayer.vida <= 0)
        {
            chromaticAberration.active = true;
            grain.active = true;
            
            StartCoroutine("startMuerte");

            animator.SetTrigger("morir");
        }
    }

    public IEnumerator setGolpeadoFalse()
    {
        yield return new WaitForSeconds(1f);

        golpeado = false;
    }

    IEnumerator startMuerte()
    {
        rigidbody.velocity = Vector2.zero;
        player2DMovement.mov = true;
        player2DMovement.golpeado = true;
        player2DAtack.atacando = false;
        player2DAtack.shoot = false;
        golpeado = true;
        
        yield return new WaitForSeconds(1f);
        
        CM.m_Lens.Dutch = -20;
        CM.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(
            CM.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x, 0.18f,
            CM.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
        CM.m_Lens.OrthographicSize = 0.04f;
        
        yield return new WaitForSeconds(0.002f);

        ojos.SetActive(true);

        Time.timeScale = 0.1f;
        
        yield return new WaitForSeconds(0.1f);

        canvasMuerte.enabled = true;
        
        Cursor.visible = true;
    }
}
