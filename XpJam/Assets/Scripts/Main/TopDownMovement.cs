using System;
using UnityEngine;

public class TopDownMovement : MonoBehaviour 
{
    private Rigidbody2D _rigidbody;
    public Vector2 Direction;
    public float Acceleration;
    public float Decceleration;
    public float MaxSpeed;

    private void Awake() 
    {
        Direction = Vector2.zero;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() => Move(Direction);

    private void Move(Vector2 direction)
    {
        Accelerate();
        SpeedCutOff();
        Decelerate();
    }
    private void SpeedCutOff()
    {
        if (_rigidbody.velocity.magnitude > MaxSpeed)
            _rigidbody.velocity = new(Mathf.Sign(_rigidbody.velocity.x) * MaxSpeed, MathF.Sign(_rigidbody.velocity.y) * MaxSpeed);
    }
    private void Decelerate()
    {
        if (Direction == Vector2.zero
            || Direction.x > 0 && _rigidbody.velocity.x < 0
            || Direction.x < 0 && _rigidbody.velocity.x > 0
            || Direction.y > 0 && _rigidbody.velocity.y < 0
            || Direction.y < 0 && _rigidbody.velocity.y > 0)
            _rigidbody.velocity = new(_rigidbody.velocity.x * Decceleration, _rigidbody.velocity.y * Decceleration);
    }
    private void Accelerate() 
        => _rigidbody.AddForce(new(Direction.x * Acceleration, Direction.y * Acceleration));
    public void StopAll() => Direction = Vector2.zero;
}