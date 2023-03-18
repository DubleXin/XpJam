using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownMovement : MonoBehaviour 
{
    private Rigidbody2D _rigidbody;
    [Header("Movement params")]
    [SerializeField]
    private Vector2 _direction;
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _linearDrag;
    [SerializeField]
    private float _maxSpeed;

    private void Awake() 
    {
        _direction = Vector2.zero;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() => Move();

    private void Move()
    {
        Accelerate();
        SpeedCutOff();
        LinearDrag();
    }
    private void SpeedCutOff()
    {
        if(Mathf.Abs(_rigidbody.velocity.x) > _maxSpeed)
            _rigidbody.velocity = new(Mathf.Sign(_rigidbody.velocity.x)*_maxSpeed,_rigidbody.velocity.y);
        if (Mathf.Abs(_rigidbody.velocity.y) > _maxSpeed)
            _rigidbody.velocity = new(_rigidbody.velocity.x, Mathf.Sign(_rigidbody.velocity.y) * _maxSpeed);
    }
    private void LinearDrag()
    {
        if (_direction == Vector2.zero || Mathf.Abs(_direction.x) <= 0.4 && Mathf.Abs(_direction.y) <= 0.4)
            _rigidbody.drag = _linearDrag;
        else
            _rigidbody.drag = 0f;
    }
    public void OnMove(InputAction.CallbackContext context) => _direction = context.ReadValue<Vector2>();
    private void Accelerate() 
        => _rigidbody.AddForce(new(_direction.x * _acceleration, _direction.y * _acceleration));
    public void StopAll() => _direction = Vector2.zero;
}