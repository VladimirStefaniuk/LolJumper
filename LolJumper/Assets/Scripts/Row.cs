using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))] 
public class Row : MonoBehaviour {

    public static event Action OnCameraExit;
    public static event Action OnCameraEnter;

    // Use this for initialization
    void Awake () {
        GetComponent<BoxCollider2D>().isTrigger = true; 
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.tag == "CameraEnter")
        {
            Debug.LogWarning("2 OnTriggerEnter CameraEnter");
            GameEvents.OnCameraRowEnter.TryAction();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CameraExit")
        {
            ReleaseBlocks();
        }
    }

    void ReleaseBlocks()
    {
        foreach (Transform _child in transform)
        {
            _child.gameObject.SetActive(false);
        }
        transform.DetachChildren(); 
        this.gameObject.SetActive(false);
    }
}
