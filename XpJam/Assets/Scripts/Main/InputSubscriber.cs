using UnityEngine;

public class InputSubscriber : MonoBehaviour
{
    private InputHandler _inputHandler;
    private TopDownMovement _playerTopDownMovement;
    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inputHandler = new InputHandler();
        _playerTopDownMovement = _player.GetComponent<TopDownMovement>();
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerCombat = _player.GetComponent<PlayerCombat>();
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
        _inputHandler.Player.Crouch.canceled += _playerMovement.OnCrouch;
        _inputHandler.Player.Dash.started += _playerMovement.OnDash;
        _inputHandler.Player.Interact.started += _player.GetComponent<Sensor>().Activate;
        _inputHandler.Player.Attack.started += _playerCombat.MeleeAtack;
        _inputHandler.Player.Look.performed += _playerCombat.SetLookDirection;
    }
    public void UnsubscribeCharacterDependant()
    {
        _inputHandler.Player.Movement.performed -= _playerTopDownMovement.OnMove;
        _inputHandler.Player.Movement.canceled -= _playerTopDownMovement.OnMove;
        _inputHandler.Player.Crouch.started -= _playerMovement.OnCrouch;
        _inputHandler.Player.Crouch.canceled -= _playerMovement.OnCrouch;
        _inputHandler.Player.Dash.started += _playerMovement.OnDash;
        _inputHandler.Player.Interact.started -= _player.GetComponent<Sensor>().Activate;
        _inputHandler.Player.Attack.started -= _playerCombat.MeleeAtack;
        _inputHandler.Player.Look.performed -= _playerCombat.SetLookDirection;
    }
    private void OnEnable()
    {
        _inputHandler.Player.Movement.Enable();
        _inputHandler.Player.Crouch.Enable();
        _inputHandler.Player.Dash.Enable();
        _inputHandler.Player.Interact.Enable();
        _inputHandler.Player.Attack.Enable();
        _inputHandler.Player.Look.Enable();
    }
    private void OnDisable()
    {
        _inputHandler.Player.Movement.Disable();
        _inputHandler.Player.Crouch.Disable();
        _inputHandler.Player.Dash.Disable();
        _inputHandler.Player.Interact.Disable();
        _inputHandler.Player.Attack.Disable();
        _inputHandler.Player.Look.Disable();
    }
}