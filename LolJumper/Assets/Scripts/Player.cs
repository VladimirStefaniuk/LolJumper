using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            { 
                _instance = GameObject.FindObjectOfType<Player>();
                if (_instance == null)
                {
                    Debug.LogError("Missing Player script in the scene!");
                }
            }
            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    [HideInInspector]
    public float Radius;
    private TrailRenderer _trail;

    // Use this for initialization
    void Awake () {
        if (_instance == null)
            _instance = this;

        _trail = GetComponent<TrailRenderer>();

        GameEvents.OnPlayerFall += () => _trail.enabled = false;
        GameEvents.OnGameRestart += () => _trail.enabled = true; 
    }
	 
}
