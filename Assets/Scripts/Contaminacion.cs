using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contaminacion : MonoBehaviour
{
    public float alphaActual = 0.0f;
    public float alphaMaximo = 1.0f;

    private Image imageContaminacion;

    private void Start()
    {
        imageContaminacion = GetComponent<Image>();
    }

    private void Update()
    {

        //Limitamos el alpha al valor maximo
        alphaActual = Mathf.Min(alphaActual, alphaMaximo);

        //Actualizamos el color/alpha de la imagen 
        Color colorActual = imageContaminacion.color;
        colorActual.a = alphaActual;
        imageContaminacion.color = colorActual;

        if(alphaActual >= alphaMaximo)
        {
            PerderJuego();
        }

    }

    private void PerderJuego()
    {
        //Cambiamos a la escena de Scores
        Debug.Log("Perdimos");
    }
}