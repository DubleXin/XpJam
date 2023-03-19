using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TopDownMovement))]
public class CloseCombatDamageDealerAI : MonoBehaviour
{
    [Header("Main Stats")]
    [SerializeField] private Stat _damage;
    [SerializeField] private GameObject _slash;
    [SerializeField] private float _noticeRange;
    [SerializeField] private float _meleeCoolDown;
    [SerializeField] private float _meleeRange;
    [SerializeField] private float _meleRereactionTime;
    [SerializeField] private float _movementCoolDown;
    [SerializeField] private float _localSlashPositionX;
    [SerializeField] private float _localSlashPositionY;

    private GameObject _target;
    private TopDownMovement _movement;
    private bool _isMovementCoolDown;
    private bool _isMeleeCoolDown;
    private PlayerMovement _playerMovement;
    private GameObject m_player
    {
        get
        {
            if (_target is null)
                _target = GameObject.FindGameObjectWithTag("Player");
            return _target;
        }
    }
    private void OnEnable()
    {
        IsPursuit = true;
    }
    public bool IsPursuit { get; private set; }

    private void Awake() 
    {
        _movement = GetComponent<TopDownMovement>();
        _playerMovement = m_player.GetComponent<PlayerMovement>();
    }
    private void FixedUpdate()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, m_player.transform.position - transform.position, _noticeRange, LayerMask.GetMask("Player"));
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, m_player.transform.position - transform.position, _noticeRange, LayerMask.GetMask("Occlude"));
        RaycastHit2D hitHideaway = Physics2D.Raycast(transform.position, m_player.transform.position - transform.position, _noticeRange, LayerMask.GetMask("Semi_Occlude"));

        if (Vector2.Distance(m_player.transform.position, transform.position) < _meleeRange)
        {
            if (!_isMeleeCoolDown && hitPlayer)
                StartCoroutine(Melee());
        }
        if (!_isMovementCoolDown)
            if ((m_player.transform.position - transform.position).magnitude < _noticeRange)
            {
               // Debug.Log($"{hitPlayer} {hitWall} {hitHideaway}");
                if (hitPlayer && ((!hitWall && !hitHideaway) || ( !hitWall && hitHideaway && !_playerMovement.IsCrouching) 
                    || (hitWall && hitPlayer.distance < hitWall.distance)))
                {
                    Debug.DrawRay(transform.position, m_player.transform.position - transform.position, Color.blue);
                    _movement.OnMove((m_player.transform.position - transform.position));
                }
                else
                {
                    Debug.DrawRay(transform.position, m_player.transform.position - transform.position, Color.red);
                    IsPursuit = false;
                }
                
            }
            else
                _movement.OnMove(Vector2.zero);
    }
    public void DealDamage() => StartCoroutine(Melee());
    private IEnumerator Melee()
    {
        yield return new WaitForSeconds(_meleRereactionTime);
        _isMeleeCoolDown = true;
        _isMovementCoolDown = true;
        Transform slash = Instantiate(_slash, transform).transform;
        slash.localPosition = new Vector3(_localSlashPositionX, _localSlashPositionY);
        slash.rotation = Angle.FromAtoB(transform.position, m_player.transform.position + new Vector3(0, 0.1f));
        yield return new WaitForSeconds(_movementCoolDown);
        _isMovementCoolDown = false;
        yield return new WaitForSeconds(_meleeCoolDown);
        _isMeleeCoolDown = false;
    }
}
