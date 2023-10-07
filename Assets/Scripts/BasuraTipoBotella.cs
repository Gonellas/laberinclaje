using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasuraTipoBotella : BasuraFactoryMethod
{
    public GameObject basuraTipoBotella; // Asigna el prefab de la Basura Tipo A en el inspector

    public override GameObject CreateBasura()
    {
        return basuraTipoBotella;
    }
}
