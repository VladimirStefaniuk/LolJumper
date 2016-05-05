using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public static bool isGameActive = true;

    void Awake()
    {
        isGameActive = true;
        GameEvents.OnPlayerFall += PlayerFall;
    }

    void PlayerFall()
    {
        isGameActive = false;
        //Time.timeScale = 0;
    }
}
