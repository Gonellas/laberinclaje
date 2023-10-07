using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basura : MonoBehaviour
{
    public BasuraFactoryMethod[] factoryMethods; // Referencia a la clase Factory
    public Transform puntoSpawn; // Punto de inicio en la parte superior de la pantalla
    public float minSpawnIntervalo = 1.0f; // Intervalo mínimo entre la generación de basura
    public float maxSpawnIntervalo = 5.0f; // Intervalo máximo entre la generación de basura
    public float anchoMaximoSpawn;
    public float anchoMinimoSpawn;

    // Lista que almacena las basuras generadas
    private List<GameObject> basurasGeneradas = new List<GameObject>();

    private float proximoTiempoSpawn;

    private void Start()
    {
        proximoTiempoSpawn = Random.Range(minSpawnIntervalo, maxSpawnIntervalo);
    }

    private void Update()
    {
        if (Time.time >= proximoTiempoSpawn)
        {
            SpawnBasura();
            proximoTiempoSpawn = Time.time + Random.Range(minSpawnIntervalo, maxSpawnIntervalo);
        }
        DestruirBasuraFueraDeCamara();
    }

    private void SpawnBasura()
    {
        // Elegir aleatoriamente una fábrica de basura del array
        BasuraFactoryMethod selectedFactory = factoryMethods[Random.Range(0, factoryMethods.Length)];

        // Utilizar la fábrica seleccionada para crear la basura
        GameObject basuraPrefab = selectedFactory.CreateBasura();

        // Generar una posición aleatoria en el eje X
        Vector3 spawnPosition = puntoSpawn.position + new Vector3(Random.Range(anchoMinimoSpawn, anchoMaximoSpawn), 0f, 0f);

        // Instanciar la basura en la posición generada
        GameObject newBasura = Instantiate(basuraPrefab, spawnPosition, Quaternion.identity);

        // Agregar la basura a la lista de basuras generadas
        basurasGeneradas.Add(newBasura);

        // Hacer que la basura caiga utilizando un componente Rigidbody2D
        Rigidbody2D rb = newBasura.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1.0f; // Ajustar la gravedad según sea necesario
        }
    }

    private void DestruirBasuraFueraDeCamara()
    {
        List<GameObject> basurasParaEliminar = new List<GameObject>();

        foreach (var basura in basurasGeneradas)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(basura.transform.position);

            if (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)
            {
                basurasParaEliminar.Add(basura);
            }
        }

        foreach (var basura in basurasParaEliminar)
        {
            basurasGeneradas.Remove(basura);
            Destroy(basura);
        }
    }

}