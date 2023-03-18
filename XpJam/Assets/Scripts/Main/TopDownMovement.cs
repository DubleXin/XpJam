using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownMovement : MonoBehaviour 
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Animator _animator;
    [Header("Movement params")]
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _linearDrag;
    [SerializeField] private float _maxSpeed;

    private void Awake() 
    {
        _direction = Vector2.zero;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate() => Move();

    private void Move()
    {
        Animate();
        Flip();
        Accelerate();
        SpeedCutOff();
        LinearDrag();
    }
    private void Animate()
    {
        _animator.SetFloat("Speed", (Mathf.Abs(_rigidbody.velocity.x) + Mathf.Abs(_rigidbody.velocity.y)) / 2);
        _animator.SetFloat("Horizontal", _rigidbody.velocity.x);
        _animator.SetFloat("Vertical", _rigidbody.velocity.y);
    }
    private void Flip() => _renderer.flipX = _rigidbody.velocity.x < 0;
    
    private void SpeedCutOff()
    {
        if (Mathf.Abs(_rigidbody.velocity.x) > _maxSpeed)
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