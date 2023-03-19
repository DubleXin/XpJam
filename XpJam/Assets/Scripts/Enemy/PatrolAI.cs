using UnityEngine;
using System.Collections;

public class PatrolAI : MonoBehaviour
{
    private enum PatrolState
    {
        PATROL,
        PURSUIT
    }
    [SerializeField] private float _visionRadius;
    [SerializeField] private Vector2[] _patrolDestinations;
    [SerializeField] private GameObject _patrolPointsHandler;
    private SubPointMesh _subPointMesh;
    private CloseCombatDamageDealerAI _combatAI;
    private bool _isWaiting;
    private int _currentDestination = 0;
    private PatrolState _state = PatrolState.PATROL;
    private TopDownMovement _movement;
    private void Start()
    {
        _movement = GetComponent<TopDownMovement>();
        _combatAI = GetComponent<CloseCombatDamageDealerAI>();
        if (_patrolPointsHandler.TryGetComponent(out _subPointMesh))
            _patrolDestinations = _subPointMesh.Points;
        StartCoroutine(SlowUpdate());
    }
    private IEnumerator SlowUpdate()
    {
        while (true) 
        {
            RaycastHit2D[] hit2D = Physics2D.CircleCastAll(Vector2.zero, _visionRadius, Vector2.zero, 0, LayerMask.NameToLayer("Player"));
            GameObject player;
            PlayerMovement playerMovement;
            
            if (hit2D.Length > 0)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player.transform.gameObject.TryGetComponent(out playerMovement))
                {

                    bool hitPlayer = Physics2D.Raycast(transform.position, player.transform.position - transform.position, _visionRadius, LayerMask.GetMask("Player"));
                    bool hitWall = Physics2D.Raycast(transform.position, player.transform.position - transform.position, _visionRadius, LayerMask.GetMask("Occlude"));
                    bool hitHideaway = Physics2D.Raycast(transform.position, player.transform.position - transform.position, _visionRadius, LayerMask.GetMask("Semi_Occlude"));

                    if (_state == PatrolState.PATROL && playerMovement != null && hitPlayer && ((!hitWall && !hitHideaway) || (!hitWall && hitHideaway && !playerMovement.IsCrouching)))
                    {
                        _state = PatrolState.PURSUIT;
                    }
                }
            }
            else if (_state == PatrolState.PURSUIT && hit2D.Length == 0)
            {
                _state = PatrolState.PATROL;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void FixedUpdate()
    {
        if(_combatAI.enabled && _state == PatrolState.PURSUIT)
        {
            if (_combatAI.IsPursuit == false)
                StopPursuit();
            else
                return;
        }
        if (_state == PatrolState.PATROL) Patrol();
        else Pursuit();
    }
    private void Patrol()
    {
        if (_isWaiting)
            return;

        if (_currentDestination < _patrolDestinations.Length)
        {
            if (Vector2.Distance(transform.position, _patrolDestinations[_currentDestination]) > 1f)
            {
                _movement.OnMove(new Vector2
                (
                    _patrolDestinations[_currentDestination].x - transform.position.x,
                    _patrolDestinations[_currentDestination].y - transform.position.y)
                );
            }
            else
            {
                StartCoroutine(TakeLook());
                _currentDestination++;
            }
        }
        else _currentDestination = 0;
    }
    private IEnumerator TakeLook() 
    {
        _movement.OnMove(Vector2.zero);
        _isWaiting = true;
        yield return new WaitForSeconds(2f);
        _isWaiting = false;
    }
    protected virtual void StopPursuit()
    {
        _state = PatrolState.PATROL;
        _combatAI.enabled = false;
    }
    protected virtual void Pursuit() 
    {
        _combatAI.enabled = true;
    }
}