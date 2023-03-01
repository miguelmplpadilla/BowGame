using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPistolaController : MonoBehaviour
{
    private GameObject player;

    public bool mov = true;
    private bool girando = false;
    public bool atacando = false;
    public bool muerto = false;

    public float speed;

    private Vector2 escala;

    private Animator animator;
    private Enemigo2DHurtController hurtController;
    public GameObject manchaSangre;
    public Color colorSangre;

    public GameObject bala;
    public GameObject shootPoint;
    public float fuerzaBala;

    private void Awake()
    {
        escala = new Vector2(1, 1);
        transform.localScale = escala;

        animator = GetComponent<Animator>();
        hurtController = GetComponentInChildren<Enemigo2DHurtController>();
    }

    private void Start()
    {
        player = GameObject.Find("Player2D");
    }

    void Update()
    {
        if (mov && !muerto)
        {
            Vector2 playerHorizontalPosition = new Vector2(player.transform.position.x, transform.position.y);
            Vector2 playerVerticalPosition = new Vector2(transform.position.x, player.transform.position.y);
            
            float distanciaHorizontal = Vector2.Distance(transform.position, playerHorizontalPosition);

            float distanciaVertical = Vector2.Distance(transform.position, playerVerticalPosition);
            

            if (distanciaHorizontal < 2 && distanciaVertical < 0.2f)
            {
                if (distanciaHorizontal > 1f)
                {
                    if (!atacando)
                    {
                        Vector2 seguirPlayer = new Vector2(player.transform.position.x, transform.position.y);
                        transform.position = Vector2.MoveTowards(transform.position, seguirPlayer, speed * Time.deltaTime);
                    
                        if (player.transform.position.x > transform.position.x)
                        {
                            escala = new Vector2(1, 1);
                        }
                        else if (player.transform.position.x < transform.position.x)
                        {
                            escala = new Vector2(-1, 1);
                        }
                    
                        transform.localScale = escala;
                    
                        animator.SetBool("run", true);
                    }
                }
                else
                {
                    if (!atacando)
                    {
                        if (player.transform.position.x > transform.position.x)
                        {
                            escala = new Vector2(1, 1);
                        }
                        else if (player.transform.position.x < transform.position.x)
                        {
                            escala = new Vector2(-1, 1);
                        }
                        
                        transform.localScale = escala;
                        
                        StartCoroutine("atacar");
                        atacando = true;
                        mov = false;
                    }
                    
                    animator.SetBool("run", false);
                }
            }
            else
            {
                animator.SetBool("run", false);
            }
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    public void disparar()
    {
        GameObject balaInstanciada = Instantiate(bala, shootPoint.transform.position, Quaternion.identity);
        
        balaInstanciada.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, 0) * fuerzaBala, ForceMode2D.Impulse);
    }

    private IEnumerator atacar()
    {
        animator.SetTrigger("apuntar");
        
        yield return new WaitForSeconds(1.5f);
        
        animator.SetTrigger("disparar");

        yield return new WaitForSeconds(3f);

        atacando = false;
        mov = true;

        yield return null;
    }

    public void setMovEnemigoTrue()
    {
        mov = true;
    }

    public void destruir()
    {
        GameObject manchaSangreInstanciada = Instantiate(manchaSangre);
        manchaSangreInstanciada.transform.position = transform.position + new Vector3(0,0.2f,0);
        manchaSangreInstanciada.GetComponent<SpriteRenderer>().color = colorSangre;
        
        Destroy(gameObject);
    }
}
