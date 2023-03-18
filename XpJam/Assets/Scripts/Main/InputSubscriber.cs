using UnityEngine;
using UnityEngine.InputSystem;
public class InputSubscriber : MonoBehaviour
{
    private InputHandler _inputHandler;
    private TopDownMovement _playerMovement;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inputHandler = new InputHandler();
        _playerMovement = _player.GetComponent<TopDownMovement>();
        SubscribeNecessaries();
        SubscribeCharacterDependant();
    }
    private void SubscribeNecessaries()
    {
    }
    private void SubscribeCharacterDependant()
    {
        _inputHandler.Player.Movement.performed += _playerMovement.OnMove;
        _inputHandler.Player.Movement.canceled += _playerMovement.OnMove;
        _inputHandler.Player.Interact.started += _player.GetComponent<Sensor>().Activate;
    }
    public void UnsubscribeCharacterDependant()
    {
        _inputHandler.Player.Movement.performed -= _playerMovement.OnMove;
        _inputHandler.Player.Movement.canceled -= _playerMovement.OnMove;
        _inputHandler.Player.Interact.started -= _player.GetComponent<Sensor>().Activate;
    }
    private void OnEnable()
    {
        _inputHandler.Player.Movement.Enable();
        _inputHandler.Player.Interact.Enable();
    }
    private void OnDisable()
    {
        _inputHandler.Player.Movement.Disable();
        _inputHandler.Player.Interact.Disable();
    }
}