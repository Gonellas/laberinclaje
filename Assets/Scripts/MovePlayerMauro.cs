using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerMauro : MonoBehaviour
{
    //Variables para cambiar de tacho
    private int contadorDeClick = 0;

    //Varialbes para el movimiento
    private float horizontal;
    private float velocidad = 10f;
    private bool mirandoDerecha = true;
    private float limiteIzquierdo = -8.5f;
    private float limiteDerecho = 8.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    //Variables para el Dash
    private bool puedeDashear = true;
    [SerializeField] public bool sePuedeMover = true;
    private bool estaDasheando;
    private float velocidadDash = 24f;
    private float tiempoDash = 0.2f;
    private float dashCooldown = 1f;

    //Variable para la puntiacion
    public GestorDePuntuacion gestorPuntuacion;

    //Variable para acceder al selector de Tachos UI
    public SelectorDeTacho tachoSelector;

    public Contaminacion contaminacion;

    public BarraDash barraDash;

    public AudioSource audioMove;
    [SerializeField] private AudioClip pasoUno;
    [SerializeField] private AudioClip pasoDos;
    [SerializeField] private AudioClip feedbackBasura;


    private void Start()
    {
        contaminacion = GameObject.FindObjectOfType<Contaminacion>();

        barraDash = GameObject.FindObjectOfType<BarraDash>();
    }
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
        int animacionActual = animator.GetInteger("AnimacionActual");

        if (collision.gameObject.CompareTag("BasuraTipoBanana") && animacionActual == 0)
        {
            gestorPuntuacion.ActualizarPuntuacion(10);
            barraDash.valorActualDash += 0.35f;

            if (contaminacion.alphaActual > 0)
            {
                contaminacion.alphaActual -= 0.1f;
            }
        }

        else if (collision.gameObject.CompareTag("BasuraTipoPapel") && animacionActual == 1)
        {
            gestorPuntuacion.ActualizarPuntuacion(25);
            barraDash.valorActualDash += 0.35f;

            if (contaminacion.alphaActual > 0)
            {
                contaminacion.alphaActual -= 0.1f;
            }
        }

        else if (collision.gameObject.CompareTag("BasuraTipoBotella") && animacionActual == 2)
        {
            gestorPuntuacion.ActualizarPuntuacion(50);
            barraDash.valorActualDash += 0.35f;

            if (contaminacion.alphaActual > 0)
            {
                contaminacion.alphaActual -= 0.1f;
            }
        }

        else if (collision.gameObject.CompareTag("BasuraTipoPila") && animacionActual == 3)
        {
            gestorPuntuacion.ActualizarPuntuacion(100);
            barraDash.valorActualDash += 0.35f;

            if (contaminacion.alphaActual > 0)
            {
                contaminacion.alphaActual -= 0.1f;
            }    
        }
        else
        {
            // si el jugador colisiona con otra basura mientras esta en una animacion (sosteniendo el tacho) erroneo aumenta la contaminacion
            contaminacion.alphaActual += 0.1f;
        }
  
        // barraDash.valorActualDash += 0.1f; //Para que cualquier basura sume para el dash
        audioMove.PlayOneShot(feedbackBasura);
        Destroy(collision.gameObject);
    }

    private void Move()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        //Calculamos la nueva posicion del jugador
        Vector3 nuevaPosicion = transform.position + new Vector3(horizontal * velocidad * Time.deltaTime, 0, 0);

        //aplicamos restricciones en el eje X
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho);

        //asignar la nueva posicion del pj
        transform.position = nuevaPosicion;

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
        // Llamar a la funci�n en el TachoSelector para seleccionar el tacho
        tachoSelector.SeleccionarTacho(contadorDeClick);

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
      
        if (barraDash.EstaCasiLLena())
        {
            
            sePuedeMover = false;
            puedeDashear = false;
            estaDasheando = true;
            animator.SetBool("move", false);
            animator.SetTrigger("Dash");
            rb.velocity = new Vector2(transform.localScale.x * velocidadDash, 0f);

            if (contaminacion.alphaActual > 0)
            {
                contaminacion.alphaActual -= 0.2f;
            }

            barraDash.UsarDash();
            gestorPuntuacion.ActualizarPuntuacion(150);
        }

        yield return new WaitForSeconds(tiempoDash);
        sePuedeMover = true;
        estaDasheando = false;
        yield return new WaitForSeconds(dashCooldown);

            puedeDashear = true;
        
    }

    public void FootstepsOne()
    {
        audioMove.PlayOneShot(pasoUno);
    }

    public void FootstepsTwo()
    {
        audioMove.PlayOneShot(pasoDos);
    }
    
}
