using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestorDePuntuacion : MonoBehaviour
{
    public TMP_Text puntuacionTexto;
    public int puntuacion = 0;

    public void ActualizarPuntuacion(int valor)
    {
        puntuacion += valor;
        puntuacionTexto.text = "Score: " + puntuacion;
        // Guardar el valor del puntaje
        PlayerPrefs.SetInt("Puntaje", puntuacion); //Almacena
        PlayerPrefs.Save(); // Asegura que se guarde
    }
}
