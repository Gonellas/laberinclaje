using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagerController : MonoBehaviour
{
    public GameObject[] allMenues;
    public GameObject wantedActiveMenu;

    // variables para el temporizador
    public float tiempoTranscurrido = 0f;
    public TextMeshProUGUI textoTiempo;
    public float duracionMaxima = 10f;

    private void Start()
    {
        if(allMenues.Length > 0)
        {         
            foreach (GameObject menu in allMenues)
            {
                menu.SetActive(false);
            }
        }

        if (wantedActiveMenu != null) wantedActiveMenu.SetActive(true);
    }

    private void Update()
    {
        tiempoTranscurrido += Time.deltaTime;

        if(tiempoTranscurrido >= duracionMaxima)
        {
            SceneManager.LoadScene("SaveScore");
        }

        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void ChangeScene(Object scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}