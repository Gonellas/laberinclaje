using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasuraTipoBanana : BasuraFactoryMethod
{
    public GameObject basuraTipoBanana; // Asigna el prefab de la Basura Tipo A en el inspector

    public override GameObject CreateBasura()
    {
        return basuraTipoBanana;
    }
}
