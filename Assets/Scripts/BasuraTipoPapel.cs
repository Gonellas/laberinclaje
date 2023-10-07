using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasuraTipoPapel : BasuraFactoryMethod
{
    public GameObject basuraTipoPapel; // Asigna el prefab de la Basura Tipo A en el inspector

    public override GameObject CreateBasura()
    {
        return basuraTipoPapel;
    }
}
