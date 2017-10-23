using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    public PlayerController thePlayer;
    public DeathMenu theDeathScreen;
    public GameObject pauseButton;
    public AudioSource soundtrack;

    private GameObject[] enemy;
    private GameObject[] evilNinstars;
    private Vector3 playerStartPoint;
    private Vector3 platformStartPoint;
    private PlatformDestroyer[] platformList;
    private ScoreManager theScoreManager;

    void Start() {

        soundtrack.Play();

        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    public void RestartGame()
    {
        thePlayer.gameObject.SetActive(false);
        thePlayer.GetComponent<PlayerController>().enabled = true;

        enemy = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].GetComponent<EvilNinjaController>().EnemyInactive();
        }

        evilNinstars = GameObject.FindGameObjectsWithTag("KillStar");

       for (int i = 0; i < evilNinstars.Length; i++)
       {
           evilNinstars[i].SetActive(false);
       }

        theDeathScreen.gameObject.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Reset()
    {
        theDeathScreen.gameObject.SetActive(false);
        platformList = FindObjectsOfType<PlatformDestroyer>();

        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;

        thePlayer.gameObject.SetActive(true);
        thePlayer.isDead = false;
        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
        pauseButton.SetActive(true);
    }

    
}
