using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject enemiesGen;
    [SerializeField] private GameObject player; 
    [SerializeField] private Text startTime;    //game starts in 3 seconds
    [SerializeField] private Text gameTime;     //game ends through 30 seconds
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private Text totalScore;   //which is shown on the menuScreeen
    private bool gameOver = false;
    private float currTime = 3f;                //game starts in 3 seconds
    private float gameSeconds = 30f;            //game ends through 30 seconds
    // Start is called before the first frame update
    void Awake()
    {
        enemiesGen.SetActive(false);
        UnlockCursor();
    }
    void UnlockCursor()
    {
        //unlock the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void LockCursor()
    {
        //lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //timer at the beginning (3seconds)
        if (currTime > 0)
        {
            currTime -= Time.deltaTime;
            startTime.text = $"{currTime:F0}";
        }
        else if (currTime <= 0 && !gameOver)
        {
            LockCursor();
            player.GetComponent<Player>().enabled = true;
            //starts origin game timer (30s)
            TimerTick();
            enemiesGen.SetActive(true);
            //starts respawning enemies
            if (enemiesGen.GetComponent<EnemiesGen>().cancelled)
                enemiesGen.GetComponent<EnemiesGen>().StartInvoke();
            startTime.enabled = false;      
        }
    }

    void TimerTick()
    {
        gameSeconds -= Time.deltaTime;
        gameTime.text = $"{gameSeconds:F0}";
        if (gameSeconds <= 0)
        {
            gameOver = true;
            UnlockCursor();
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        enemiesGen.GetComponent<EnemiesGen>().GameOver = true;
        player.GetComponent<Player>().gameOver = true;
        var enemy = GameObject.FindWithTag("Respawn");
        if (enemy != null)
            enemy.GetComponent<Enemy>().GameOver =true;
        totalScore.text += enemiesGen.GetComponent<EnemiesGen>().currScore.text;
        menuScreen.SetActive(true);
        
        
    }

    public void OnRestartClick()
    {
        //restart the game
        SceneManager.LoadScene("SampleScene");
    }
}
