using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDash : MonoBehaviour
{
    public Slider sliderDash;
    public float valorActualDash = 0.0f;
    public float valorMaximoDash = 0.99f;

    private void Start()
    {
        sliderDash.value = valorActualDash / valorMaximoDash;
    }

    private void Update()
    {
        if(valorActualDash < valorMaximoDash)
        {

            valorActualDash = Mathf.Min(valorActualDash, valorMaximoDash);
        }

        sliderDash.value = valorActualDash / valorMaximoDash;
    }

    public void ReiniciarBarra()
    {
        valorActualDash = 0.0f;
        sliderDash.value = 0.0f;
    }

    public void UsarDash()
    {
        if (valorActualDash >= valorMaximoDash)
        {
            valorActualDash = 0.0f;
            sliderDash.value = 0.0f;
        }
    }
    public bool EstaCasiLLena()
    {
        return valorActualDash >= (valorMaximoDash * 0.99f);
    }
}
