using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string playGameLevel;

    public AudioSource buttonPressSound;
    public AudioSource backgroundMusic;


    private void Start()
    {
        backgroundMusic.Play();
    }

	public void PlayGame()
    {
        buttonPressSound.Play();
        SceneManager.LoadSceneAsync(playGameLevel);
    }

    public void QuitGame()
    {
        buttonPressSound.Play();
        Application.Quit();
    }
}
