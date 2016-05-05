using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    public Text ScoreLabel; 
    public GameObject LoseScreen;
    private int currentScore;
    private float levelPlayedTime;
    private bool isGameActive;
 
    void Awake()
    { 
        GameEvents.OnPlayerFall += GameOver; 
    }

    void Start()
    {
        currentScore = 0;
        levelPlayedTime = 0;
        isGameActive = true;
        LoseScreen.SetActive(false);
    }
	 
	void Update () {
        if (isGameActive)
        { 
            ScoreLabel.text = "Score: " + ((int)GameState.timeLevelRuning).ToString();
        }
    }

    void GameOver()
    {
        LoseScreen.SetActive(true);
        isGameActive = false;
    }

    public void PlayAgain()
    {
        GameEvents.OnGameRestart(); 
        Start();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
