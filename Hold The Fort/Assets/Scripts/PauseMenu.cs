using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool soundOn = true;
    public static bool musicOn = true;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject deathUI;


    private void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        isPaused = true;
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        isPaused = false;
    }
  
    public void SaveGame()
    {
        //do this later
    }

    public void DeathScreen()
    {
        isPaused = true;
        Time.timeScale = 0;
        deathUI.SetActive(true);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    //this method is only used in the main menu
    public void StartGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene("Game");
    }

}
