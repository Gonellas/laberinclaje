using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public MovePlayerMauro playerMove;
    [SerializeField] GameObject pauseMenu;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
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
