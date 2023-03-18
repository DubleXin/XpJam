using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
     private TopDownMovement _playerMovement;
    [Header("Movement params")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _linearDrag;
    [SerializeField] private float _movementRate;
    [SerializeField] private float _accelerationRate;
    [SerializeField] private float _linearDragRate;
    [SerializeField] private bool _isCrouching;
    private void Awake()
    {
       _playerMovement = GetComponent<TopDownMovement>();
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