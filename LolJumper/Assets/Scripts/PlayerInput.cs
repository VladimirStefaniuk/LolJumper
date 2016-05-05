using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player input and apply it to the ball
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput : MonoBehaviour {

    public float JumpPower;
    public float MoveSpeed;

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _circleCollider;

    private float _hInput;
    private bool _isGrounded;
    private bool _jumpPressed;
    private Vector2 _velocityVector;
    /// <summary>
    /// TouchColliderDistance
    /// </summary>
    private float _lenghtTouchRay;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
         
        _isGrounded = true;
        GameEvents.OnGameRestart += MovePlayerToStart;
    }

	void Start () {
        if (_circleCollider != null)
        {
            _lenghtTouchRay = _circleCollider.radius * 1.1f;
        }
        MovePlayerToStart();
    }

    void MovePlayerToStart()
    {
        // TODO: dynamic position
        _rigidbody.velocity = Vector3.zero;
        transform.position = new Vector3(0, 2, 0);
    }
	 
	void Update () {

        _isGrounded = Physics2D.Raycast(transform.localPosition, Vector2.down, _lenghtTouchRay, 1 << LayerMask.NameToLayer("Ground"));
        _hInput = Input.GetAxis("Horizontal");
        _jumpPressed = Input.GetButtonDown("Jump");
        // x velocity
        _velocityVector.x = _hInput * MoveSpeed;
        // y velocity
        if (_isGrounded && _jumpPressed) { 
            _velocityVector.y = JumpPower;
        }
        else {
            _velocityVector.y = _rigidbody.velocity.y;
        }
        // final velocity
        _rigidbody.velocity = _velocityVector; 
    }
     
}
