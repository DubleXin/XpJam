using UnityEngine;
using UnityEngine.InputSystem;
public class InputSubscriber : MonoBehaviour
{
    private InputHandler _inputHandler;
    private TopDownMovement _playerMovement;
    private InputAction _moveAction;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inputHandler = new InputHandler();
        _moveAction = _inputHandler.Player.Movement;
        _playerMovement = _player.GetComponent<TopDownMovement>();
        SubscribeNecessaries();
        SubscribeCharacterDependant();
    }
    private void FixedUpdate()
    {
        //Debug.Log($"{_moveAction.ReadValue<Vector2>().x} { _moveAction.ReadValue<Vector2>().y}");
    }
    private void SubscribeNecessaries()
    {
    }
    private void SubscribeCharacterDependant()
    {
        _inputHandler.Player.Movement.performed += _ => _playerMovement.Direction = _.ReadValue<Vector2>();
        _inputHandler.Player.Movement.performed += _ => Debug.Log($"{_.ReadValue<Vector2>()}");
       // _inputHandler.Player.Movement.canceled += _ => _playerMovement.Direction = Vector2.zero;
    }
    public void UnsubscribeCharacterDependant()
    {
        _inputHandler.Player.Movement.performed -= _ => _playerMovement.Direction = _.ReadValue<Vector2>();
       // _inputHandler.Player.Movement.canceled -= _ => _playerMovement.Direction = Vector2.zero;
    }
    private void OnEnable()
    {
        _inputHandler.Player.Movement.Enable();
    }
    private void OnDisable()
    {
        _inputHandler.Player.Movement.Disable();
    }
}