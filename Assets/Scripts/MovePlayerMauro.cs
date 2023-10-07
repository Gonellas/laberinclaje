using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerMauro : MonoBehaviour
{
    //Varialbes para el movimiento
    private float horizontal;
    private float velocidad = 8f;
    private bool mirandoDerecha = true;

    //Variables para el Dash
    private bool puedeDashear = true;
    private bool estaDasheando;
    private float poderDash = 24f;
    private float tiempoDash = 0.2f;
    private float dashCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    //[SerializeField] private SpriteRenderer spriteRend;
    void Update()
    {
        if (estaDasheando)
        {
            return;
        }

        Move();
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

    private void Move()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        

        if(horizontal != 0f)
        {
            animator.SetBool("move", true);
            animator.SetBool("trashRed", true);
        }
        else
        {
            animator.SetBool("move", false); 
            animator.SetBool("trashRed", false);
        }
    }

    //Funcion para cambiar el tacho de basura :v
    private void CambiarTacho()
    {

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
        puedeDashear = false;
        estaDasheando = true;
        animator.SetBool("move", false);
        animator.SetBool("trashRed", false);
        animator.SetBool("dash", true);
        rb.velocity = new Vector2(transform.localScale.x * poderDash, 0f);
        yield return new WaitForSeconds(tiempoDash);
        animator.SetBool("dash", false);
        estaDasheando = false;
        yield return new WaitForSeconds(dashCooldown);
        puedeDashear = true;
        

    }

  
}
