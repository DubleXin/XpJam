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
    private bool _isWaiting;
    private int _currentDestination = 0;
    private PatrolState _state = PatrolState.PATROL;
    private TopDownMovement _movement;
    private void Start()
    {
        _movement = GetComponent<TopDownMovement>();
        StartCoroutine(SlowUpdate());
    }
    private IEnumerator SlowUpdate()
    {
        while (true) 
        {
            if (_state == PatrolState.PATROL && Physics2D.CircleCastAll(Vector2.zero,
                _visionRadius, Vector2.zero, 0, LayerMask.NameToLayer("Creature")).Length > 0)
            {
                _state = PatrolState.PURSUIT;
            }
            else if (_state == PatrolState.PURSUIT && Physics2D.CircleCastAll(Vector2.zero,
                _visionRadius, Vector2.zero, 0, LayerMask.NameToLayer("Creature")).Length == 0) 
            {
                _state = PatrolState.PATROL;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void FixedUpdate()
    {
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
    protected virtual void Pursuit() 
    {
        
    }
}
