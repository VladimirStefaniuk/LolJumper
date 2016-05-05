using UnityEngine;
using System;
using System.Collections;

public class CameraControler : MonoBehaviour {
     
    public Collider2D TopBoundCollider;
    public Collider2D BottomBoundCollider;
    public AnimationCurve DifficultyCurve;

    private float currentMoveSpeed = 1.0f;
    public float minMoveSpeed = 1.0f;
    public float maxMoveSpeed = 5.0f;
    

    private float deadY;
    public float delayBeforeStart = 1.0f;

    private IEnumerator _cameraAnimation;

 

    void Start()
    {
        TopBoundCollider.transform.position = new Vector3
            (transform.position.x,
            transform.position.y + Camera.main.orthographicSize,
            0);

        BottomBoundCollider.transform.position = new Vector3
            (transform.position.x, 
            transform.position.y - Camera.main.orthographicSize,
            0);

        StartCameraAnimation(); 
        GameEvents.OnGameRestart += StartCameraAnimation;
    }


    void StartCameraAnimation()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (_cameraAnimation != null)
        {
            StopCoroutine(_cameraAnimation);
            _cameraAnimation = null;
        }

        _cameraAnimation = AnimateCamera();
        StartCoroutine(_cameraAnimation);
    }

    IEnumerator AnimateCamera()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        while (true)
        {
            currentMoveSpeed = Mathf.Clamp(Mathf.Log10(GameState.timeLevelRuning) * 2, minMoveSpeed, maxMoveSpeed);
            var CameraMovingVector = Vector3.up * currentMoveSpeed * Time.deltaTime;
            transform.Translate(CameraMovingVector);
            deadY = transform.position.y - Camera.main.orthographicSize;

            if (Player.Instance.transform.position.y < deadY && GameState.isGameActive)
            {
                GameEvents.OnPlayerFall.TryAction();
                Debug.Log("Player fall" + Player.Instance.transform.position.y + " " + deadY);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void OnDrawGizmos()
    {
        // draw dead z plane
        var deadTrigerPos = transform.position;
        deadTrigerPos.y = deadY;
        var gizmoDefaultColor = Gizmos.color; 
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(deadTrigerPos, new Vector3(100, 0.01f, 100));
        Gizmos.color = gizmoDefaultColor;
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("1 OnTriggerEnter CameraEnter");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.LogWarning("1 OnTriggerExit CameraExit");
    }
}
