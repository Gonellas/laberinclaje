using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntryScore : MonoBehaviour
{
    public int puntajeGuardado;
    private TextMeshProUGUI txtMesh;
    private void Start()
    {
        txtMesh = GetComponent<TextMeshProUGUI>();
        CargaPuntos();
    }

    private void Update()
    {
        txtMesh.text = puntajeGuardado.ToString();
    }

    public void CargaPuntos()
    {
        // Cargar el valor del puntaje en otra escena        
        puntajeGuardado = PlayerPrefs.GetInt("Puntaje");
        txtMesh.text = puntajeGuardado.ToString();
        Debug.LogWarning(puntajeGuardado);
    }
}
