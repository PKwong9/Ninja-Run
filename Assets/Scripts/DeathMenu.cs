using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public string mainMenuLevel;

    public AudioSource buttonPressSound;

	public void RestartGame()
    {
        buttonPressSound.Play();
        FindObjectOfType<GameManager>().Reset();
    }

    public void QuitToMain()
    {
        buttonPressSound.Play();
        SceneManager.LoadSceneAsync(mainMenuLevel);
    }
}
