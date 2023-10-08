using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorDeTacho : MonoBehaviour
{
    public Image TachoVerde;
    public Image TachoAmarillo;
    public Image TachoAzul;
    public Image TachoRojo;

    private int tachoSeleccionado = 0;

    public void SeleccionarTacho(int tacho)
    {
        tachoSeleccionado = tacho;

        // Actualizar la visualización de los tachos
        TachoVerde.enabled = (tachoSeleccionado == 0);
        TachoAmarillo.enabled = (tachoSeleccionado == 1);
        TachoAzul.enabled = (tachoSeleccionado == 2);
        TachoRojo.enabled = (tachoSeleccionado == 3);
    }

    private void Start()
    {
        TachoVerde.enabled = false;
        TachoAmarillo.enabled = false;
        TachoAzul.enabled = false;
        TachoRojo.enabled = false;
    }
}
