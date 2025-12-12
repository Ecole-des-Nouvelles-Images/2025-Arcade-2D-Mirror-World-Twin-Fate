using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
public class PauseMenu : MonoBehaviour
{
    public GameObject firstselectedbutton;
    public GameObject MenuPause;
    private bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        MenuPause.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Pause()
    {
        MenuPause.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
}
