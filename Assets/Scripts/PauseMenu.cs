using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private bool _pauseMenuIsActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        _pauseMenuIsActive = !_pauseMenuIsActive;

        if (_pauseMenuIsActive == true)
        {
            Time.timeScale = 0.0f;
            SwitchMenu();
        }

        if (_pauseMenuIsActive == false)
        {
            Time.timeScale = 1.0f;
            SwitchMenu();
        }
    }

    public void SwitchMenu()
    {
        if (_pauseMenuIsActive == true)
        {
            _pauseMenu.SetActive(true);
        }

        if(_pauseMenuIsActive == false)
        {
            _pauseMenu.SetActive(false);
        }
    }
    //public void OpenMenu()
    //{
    //    Time.timeScale = 0.0f;
    //    _pauseMenu.SetActive(true);
    //}

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        _pauseMenuIsActive = false;
        _pauseMenu.SetActive(false);
    }

    public void LoadTitleScreen()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("IntroScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
