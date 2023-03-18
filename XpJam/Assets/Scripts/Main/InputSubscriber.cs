using UnityEngine;
using UnityEngine.InputSystem;
public class InputSubscriber : MonoBehaviour
{
    private InputHandler _inputHandler;
    private TopDownMovement _playerTopDownMovement;
    private PlayerMovement _playerMovement;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inputHandler = new InputHandler();
        _playerTopDownMovement = _player.GetComponent<TopDownMovement>();
        SubscribeNecessaries();
        SubscribeCharacterDependant();
    }
    private void SubscribeNecessaries()
    {
    }
    private void SubscribeCharacterDependant()
    {
        _inputHandler.Player.Movement.performed += _playerTopDownMovement.OnMove;
        _inputHandler.Player.Movement.canceled += _playerTopDownMovement.OnMove;
        _inputHandler.Player.Crouch.started += _playerMovement.OnCrouch;
        _inputHandler.Player.Interact.started += _player.GetComponent<Sensor>().Activate;
    }
    public void UnsubscribeCharacterDependant()
    {
        _inputHandler.Player.Movement.performed -= _playerTopDownMovement.OnMove;
        _inputHandler.Player.Movement.canceled -= _playerTopDownMovement.OnMove;
        _inputHandler.Player.Crouch.started -= _playerMovement.OnCrouch;
        _inputHandler.Player.Interact.started -= _player.GetComponent<Sensor>().Activate;
    }
    private void OnEnable()
    {
        _inputHandler.Player.Movement.Enable();
        _inputHandler.Player.Crouch.Enable();
        _inputHandler.Player.Interact.Enable();
    }
    private void OnDisable()
    {
        _inputHandler.Player.Movement.Disable();
        _inputHandler.Player.Crouch.Disable();
        _inputHandler.Player.Interact.Disable();
    }
}