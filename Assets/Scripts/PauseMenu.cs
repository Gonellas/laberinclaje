using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public MovePlayerMauro playerMove;
    [SerializeField] GameObject pauseMenu;

    private bool isPaused = false;

    // variables para el temporizador
    public float tiempoTranscurrido = 0f;
    public TextMeshProUGUI textoTiempo;
    public float duracionMaxima = 180f;

    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
  
        tiempoTranscurrido += Time.deltaTime;


        if (tiempoTranscurrido >= duracionMaxima)
        {
            SceneManager.LoadScene("SaveScore");
        }
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);

    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        PauseCanvas(isPaused);
    }

    public void PauseCanvas(bool pause)
    {
        if (pause)
        {
            playerMove.sePuedeMover = false;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            
        }
        else
        {
            playerMove.sePuedeMover = true;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
    
}
