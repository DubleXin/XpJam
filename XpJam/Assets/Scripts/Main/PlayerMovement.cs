using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private TopDownMovement _playerMovement;
    private Dash _playerDash;
    private Rigidbody2D _playerRigidbody;
    [Header("Movement params")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _linearDrag;
    [Header("Crouch params")]
    [SerializeField] private float _movementRate;
    [SerializeField] private float _accelerationRate;
    [SerializeField] private float _linearDragRate;
    [SerializeField] private bool _isCrouching;
    [Header("Dash params")]
    [SerializeField] private bool _canDash;
    [SerializeField] private bool _isDashing;
    [SerializeField] float _dashingPower;
    [SerializeField] private float _dashingTime;
    [SerializeField] private float _dashingCooldown;

    public bool IsCrouching { get => _isCrouching; }
    private void Awake()
    {
        _canDash = true;
        _playerMovement = GetComponent<TopDownMovement>();
        _playerDash = GetComponent<Dash>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerDash.OnCanDashChanged += CanDashChanged;
        _playerDash.OnIsDashingChanged += IsDashingChanged;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (_canDash)
            _playerDash.PerformDash(_canDash, _isDashing,
                _dashingTime, _dashingCooldown, _dashingPower,
                _playerMovement.Direction, _playerRigidbody);
    }
    private void CanDashChanged(bool value) => _canDash = value;
    private void IsDashingChanged(bool value) 
    {
        _playerMovement.enabled = !value;
        _isDashing = value;
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        _isCrouching = context.ReadValue<float>() == 1 ? true : false;
        if (_isCrouching)
            _playerMovement.SetMovementParams(_movementSpeed * _movementRate, _acceleration * _accelerationRate, _linearDrag * _linearDragRate);
        else
            _playerMovement.SetMovementParams(_movementSpeed, _acceleration, _linearDrag);
    }
}