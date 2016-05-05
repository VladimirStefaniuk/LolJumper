using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public static bool isGameActive = true;
    public static float timeLevelRuning = 0;

    void Awake()
    {
        isGameActive = true;
        GameEvents.OnPlayerFall += PlayerFall;
        GameEvents.OnGameRestart += () => { isGameActive = true; timeLevelRuning = 0; };
    }

    void PlayerFall()
    {
        isGameActive = false; 
    }

    void Update()
    {
        timeLevelRuning += Time.deltaTime;
    }
}
