using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasuraTipoPila : BasuraFactoryMethod
{
    public GameObject basuraTipoPila; // Asigna el prefab de la Basura Tipo A en el inspector

    public override GameObject CreateBasura()
    {
        return basuraTipoPila;
    }
}
