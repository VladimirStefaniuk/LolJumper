using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    public Text ScoreLabel;
    public GameObject LoseScreen;
    private int currentScore = 0;
    private bool isGameActive;
 
    void Awake()
    { 
        GameEvents.OnPlayerFall += GameOver; 
    }

    void Start()
    {
        currentScore = 0;
        isGameActive = true;
        LoseScreen.SetActive(false);
    }
	 
	void Update () {
        if (isGameActive)
        {
            currentScore = (int) Time.timeSinceLevelLoad;
            ScoreLabel.text = "Score: " + currentScore.ToString();
        }
    }

    void GameOver()
    {
        LoseScreen.SetActive(true);
        isGameActive = false;
    }

    public void PlayAgain()
    {
        // Just reload current scene
        // only temporary solution 
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
