using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public GameObject demonContainer, hudContainer, gameOverPanel;
    public TextMeshProUGUI demonCount, timeCount, endTime, countdownText;
    public bool IsGamePlaying { get; private set; }

    public int countdownTime;
    private int totalDemons, deadDemons;
    private float startTime, elapsedTime;
    TimeSpan timePlayed;

    public void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        totalDemons = demonContainer.transform.childCount;
        deadDemons = 0;
        demonCount.text = "Demons: 0 / " + totalDemons;
        IsGamePlaying = false;

        StartCoroutine(CountdownToStart());
    }

    public void SlayDemon()
    {
        deadDemons++;
        string demonCounterStr = "Demons: " + deadDemons + " / " + totalDemons;
        demonCount.text = demonCounterStr;
        
        if(deadDemons >= totalDemons)
        {
            EndGame();
        }
    }

    private void BeginGame()
    {
        IsGamePlaying = true;
        startTime = Time.time;
    }

    private void Update()
    {
        if (IsGamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlayed = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = "Time: " + timePlayed.ToString("mm':'ss'.'ff");
            timeCount.text = timePlayingStr;
        }
    }

    private void EndGame()
    {
        IsGamePlaying = false;
        Invoke("ShowGameOverScreen", 1.25f);
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        hudContainer.SetActive(false);

        string timePlayingStr = "Time: " + timePlayed.ToString("mm':'ss'.'ff");
        endTime.text = timePlayingStr;
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        BeginGame();
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

    public void OnButtonLoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
