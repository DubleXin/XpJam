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
    [Header("Range")]
    [SerializeField] private float _shootDamage;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootCoolDown;
    [SerializeField] private float _force; 
    private DamageReceiver _receiver;
    private Camera _camera;
    private Vector2 _lookingDirection;
    private bool _isMeleeCoolDown;
    private bool _isShootCoolDown;
    private Transform _gun;
    private Transform _gunMuzzle;

    private void Awake()
    {
        _camera =GameObject.FindGameObjectWithTag("Main").GetComponentInChildren<Camera>();
        _receiver = GetComponent<DamageReceiver>();
        _gun = transform.GetChild(0);
        _gunMuzzle = _gun.GetChild(0).transform;
    }
    private void FixedUpdate()
    {
        _gun.rotation = Angle.FromAtoB(_gun.position, _lookingDirection);
    }
    public void SetLookDirection(InputAction.CallbackContext context) 
    {
        _lookingDirection = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if ((_lookingDirection - new Vector2(transform.position.x, transform.position.y)).magnitude <= _meleeRange)
        {
            if (!_isMeleeCoolDown && StateMachine.LevelStatus == StateMachine.LevelStage.ON_HACKING)
                StartCoroutine(Melee());
        }
        else
        {
            if(!_isShootCoolDown)
                StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        _isShootCoolDown = true;
        Transform bullet = Instantiate(_bullet,_gunMuzzle).transform;
        bullet.localPosition = Vector3.zero;
        bullet.GetComponentInChildren<Bullet>().SetParams(_shootDamage, "Enemy");
        bullet.rotation = Angle.FromAtoB(bullet.position, _lookingDirection);
        bullet.GetComponentInChildren<Rigidbody2D>().AddForce((_lookingDirection - new Vector2(bullet.position.x, bullet.position.y)).normalized * _force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_shootCoolDown);
        _isShootCoolDown = false;
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
            receiver.TakeDamage(_melleeDamage);
        }
        yield return new WaitForSeconds(_meleeCoolDown);
        _isMeleeCoolDown = false;
    }
}
