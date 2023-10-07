using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerMauro : MonoBehaviour
{
    //Variables para cambiar de tacho
    private int contadorDeClick = 0;

    //Varialbes para el movimiento
    private float horizontal;
    private float velocidad = 8f;
    private bool mirandoDerecha = true;

    //Variables para el Dash
    private bool puedeDashear = true;
    [SerializeField] public bool sePuedeMover = true;
    private bool estaDasheando;
    private float velocidadDash = 24f;
    private float tiempoDash = 0.2f;
    private float dashCooldown = 1f;

    //Variable para la puntiacion
    public GestorDePuntuacion gestorPuntuacion;
   

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    void Update()
    {
        if (estaDasheando)
        {
            return;
        }

        if (sePuedeMover)
        {
            cambiarTacho();
            Move();
        }
        
        Flip();

        if (Input.GetKeyDown(KeyCode.LeftShift) && puedeDashear)
        {
            StartCoroutine(Dash());
        }

    }

    private void FixedUpdate()
    {
        if (estaDasheando)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * velocidad, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BasuraTipoBanana") && animator.GetInteger("AnimacionActual") == 0)
        {
            gestorPuntuacion.ActualizarPuntuacion(10);
        }

        if (collision.gameObject.CompareTag("BasuraTipoPapel") && animator.GetInteger("AnimacionActual") == 1)
        {
            gestorPuntuacion.ActualizarPuntuacion(25);
        }

        if (collision.gameObject.CompareTag("BasuraTipoBotella") && animator.GetInteger("AnimacionActual") == 2)
        {
            gestorPuntuacion.ActualizarPuntuacion(50);
        }

        if (collision.gameObject.CompareTag("BasuraTipoPila") && animator.GetInteger("AnimacionActual") == 3)
        {
            gestorPuntuacion.ActualizarPuntuacion(100);
        }

        Destroy(collision.gameObject);
    }

    private void Move()
    {

        horizontal = Input.GetAxisRaw("Horizontal");


        if (horizontal != 0f)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false); 
        }
    }

    //Funcion para cambiar el tacho de basura :v
    private void cambiarTacho()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            contadorDeClick++;
            if(contadorDeClick >= 4)
            {
                contadorDeClick = 0;
            }

            animator.SetInteger("AnimacionActual", contadorDeClick);

        }else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            contadorDeClick--;
            if(contadorDeClick < 0)
            {
                contadorDeClick = 3;
            }
            animator.SetInteger("AnimacionActual", contadorDeClick);
        }
    }

    private void Flip()
    {
        if(mirandoDerecha && horizontal < 0f || !mirandoDerecha && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            mirandoDerecha = !mirandoDerecha;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        sePuedeMover = false;
        puedeDashear = false;
        estaDasheando = true;
        animator.SetBool("move", false);
        animator.SetTrigger("Dash");
        rb.velocity = new Vector2(transform.localScale.x * velocidadDash, 0f);
        yield return new WaitForSeconds(tiempoDash);
        sePuedeMover = true;
        estaDasheando = false;
        yield return new WaitForSeconds(dashCooldown);
        puedeDashear = true;
        

    }

  
}
