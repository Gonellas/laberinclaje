using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suelo : MonoBehaviour
{

    public Contaminacion contaminacion;

    private void Start()
    {
        contaminacion = GameObject.FindObjectOfType<Contaminacion>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasuraTipoPila"))
        {
            contaminacion.alphaActual += 0.1f;
        }
        if (collision.gameObject.CompareTag("BasuraTipoBanana"))
        {
            contaminacion.alphaActual += 0.1f;
        }
        if (collision.gameObject.CompareTag("BasuraTipoPapel"))
        {
            contaminacion.alphaActual += 0.1f;
        }
        if (collision.gameObject.CompareTag("BasuraTipoBotella"))
        {
            contaminacion.alphaActual += 0.1f;
        }
    }
}
