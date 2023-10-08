using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Puntaje : MonoBehaviour
{
    public int puntos;
    private TextMeshProUGUI txtMesh;

    private void Start()
    {
        //puntos = 0;
        txtMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {       
        txtMesh.text = puntos.ToString();
    }

    public void GuardarPuntos(int puntosEntrada) //AQUI SE SUMAN LOS PUNTOS
    {
        puntos += puntosEntrada;

        // Guardar el valor del puntaje
        PlayerPrefs.SetInt("Puntaje", puntos); //Almacena
        PlayerPrefs.Save(); // Asegura que se guarde
    }
}