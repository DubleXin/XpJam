using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Melee")]
    [SerializeField] private GameObject _slash;
    [SerializeField] private float _melleeDamage;
    [SerializeField] private float _meleeCoolDown;
    [SerializeField] private float _meleeRange;
    [SerializeField] private float _localSlashPositionX;
    [SerializeField] private float _localSlashPositionY;
    private DamageReceiver _receiver;
    private Camera _camera;
    private Vector2 _lookingDirection;
    private bool _isMeleeCoolDown;

    private void Awake()
    {
        _camera =GameObject.FindGameObjectWithTag("Main").GetComponentInChildren<Camera>();
        _receiver = GetComponent<DamageReceiver>();
    }
    public void SetLookDirection(InputAction.CallbackContext context) 
    {
        _lookingDirection = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void MeleeAtack(InputAction.CallbackContext context)
    {
        if(!_isMeleeCoolDown && StateMachine.LevelStatus == StateMachine.LevelStage.ON_HACKING)
            StartCoroutine(Melee());
    }
    private IEnumerator Melee()
    {
        _isMeleeCoolDown = true;
        Transform slash = Instantiate(_slash, transform).transform;
        slash.localPosition = new Vector3(_localSlashPositionX * (_lookingDirection - new Vector2(transform.position.x, transform.position.y)).x, 
                                        _localSlashPositionY * (_lookingDirection - new Vector2(transform.position.x, transform.position.y)).y);
        slash.rotation = Angle.FromAtoB(transform.position, _lookingDirection);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _lookingDirection - new Vector2(transform.position.x, transform.position.y), _meleeRange, LayerMask.GetMask("Enemy"));
        if (hit)
        {
            DamageReceiver receiver = hit.transform.GetComponent<DamageReceiver>();
            receiver.TakeDamage(_melleeDamage, transform.gameObject);
        }
        yield return new WaitForSeconds(_meleeCoolDown);
        _isMeleeCoolDown = false;
    }
}
