using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string mainMenuLevel;

    public GameObject pauseButton;
    public GameObject pauseMenu;

    public AudioSource buttonPressSound;

    public void PauseGame()
    {
        buttonPressSound.Play();
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        buttonPressSound.Play();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void RestartGame()
    {
        buttonPressSound.Play();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        FindObjectOfType<GameManager>().Reset();
    }

    public void QuitToMain()
    {
        buttonPressSound.Play();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(mainMenuLevel);
    }
}
