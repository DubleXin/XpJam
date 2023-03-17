using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CameraState
{
    PLAYER,
    TARGET,
    FREE
};
public enum CameraMovementType
{
    INTERPOLATION,
    LINEAR,
    TELEPORT
};

public struct SequencePoint
{
    public SequencePoint(Vector2 point, CameraMovementType movementType)
    {
        Point = point;
        MovementType = movementType;
    }
    public Vector2 Point { get; }
    public CameraMovementType MovementType { get; }

    public Vector2 PointWithExtraDistance(float distance)
    {
        Vector2 ExtraDistance = new 
            (
            Point.x > 0? distance : -distance,
            Point.y > 0? distance : -distance
            );
        return Point + ExtraDistance;
    }
}

public class CameraActions : MonoBehaviour
{
    #region MainFields
    public static CameraActions Current { get; private set; }

    public CameraState State;
    public CameraMovementType MovementType;

    private Transform _target, _player;
    private float _zoomMultiplier = 1, _linearSpeed = 1, _interpolateSpeed = 5f;
    private readonly sbyte CONSTANT_RANGE = -4;
    private Vector2 _additionalDirection = Vector2.zero;
        
    public Transform Target
    {
        get 
        { 
            return _target; 
        }
        set
        {
            if (value != null)
            {
                try
                {
                    _target = value;
                }
                catch (MissingReferenceException)
                {
                    return;
                }
            }
        }
    }
    public Transform Player
    {
        get
        {
            return _player;
        }
        set
        {
            if (value != null)
            {
                try
                {
                    _player = value;
                }
                catch (MissingReferenceException)
                {
                    return;
                }
            }
        }
    }
    public float ZoomMultiplier 
    {
        get
        {
            return _zoomMultiplier;
        }
        set 
        {
            if (value > 0)
                _zoomMultiplier = value;
            else
                Debug.LogAssertion("Zoom multiplier can not be less or equal to 0");
        } 
    }
    public float LinearSpeed 
    {
        get
        {
            return _linearSpeed;
        }
        set 
        {
            if (value > 0)
                _linearSpeed = value;
            else
                Debug.LogAssertion("Linear speed can not be less equal to 0");
        }    
    }
    public float InterpolateSpeed
    {
        get
        {
            return _interpolateSpeed;
        }
        set
        {
            if (value > 0)
                _interpolateSpeed = value;
            else
                Debug.LogAssertion("Interpolation speed can not be less or equal to 0");
        }
    }

    #endregion

    #region SequenceFields

    private bool _sequenceIsRunning;
    private readonly Queue<SequencePoint> Sequence = new();

    #endregion

    #region Subscriptions
    private void Awake() => Current = this;
    #endregion

    private void Start() => RefreshPlayerLink();
    private void RefreshPlayerLink() => Player = GameObject.FindGameObjectWithTag("Player").transform;

    private void LateUpdate()
    {
        switch (State)
        {
            case CameraState.PLAYER:
                Move(Player.position);
                break;
            case CameraState.TARGET:
                Move(Target.position);
                break;
            default:
                break;
        }
    }
    private void Move(Vector2 Destination)
    {
        switch (MovementType)
        {
            case CameraMovementType.INTERPOLATION:
                Interpolate();
                break;
            case CameraMovementType.LINEAR:
                LinearMove();
                break;
            default:
                Teleport();
                break;
        }
        void Interpolate()
        {
            Vector2 InterpolatedPosition = Vector2.Lerp(transform.position, Destination + _additionalDirection, InterpolateSpeed * Time.deltaTime);
            Vector3 NewPosition = new (InterpolatedPosition.x, InterpolatedPosition.y, CONSTANT_RANGE * _zoomMultiplier);
            transform.position = NewPosition;
        }
        void LinearMove() => transform.position += LinearSpeed * 
            new Vector3
            (
                Destination.x + _additionalDirection.x - transform.position.x,
                Destination.y + _additionalDirection.y - transform.position.y,
                0
            ).normalized;

        void Teleport() => transform.position = new Vector3
            (
            Destination.x + _additionalDirection.x,
            Destination.y + +_additionalDirection.y,
            CONSTANT_RANGE * _zoomMultiplier
            );
    }

    public void AddSequencePoint(SequencePoint point)
    {
        Sequence.Enqueue(point);
        if (_sequenceIsRunning)
            return;

        StartCoroutine(PlaySequence());
    }
    private IEnumerator PlaySequence() 
    {
        _sequenceIsRunning = true;

        Vector2 destination = new();
        bool _isFinishedPlaying = true;

        while (true)
        {
            if (_isFinishedPlaying)
            {
                if (Sequence.Count == 0)
                    break;

                SequencePoint point = Sequence.Dequeue();
                destination = point.PointWithExtraDistance(0.01f);
                _isFinishedPlaying = false;
                MovementType = point.MovementType;
            }
            else if (Vector2.Distance(transform.position, destination) < 0.01f)
                _isFinishedPlaying = true;
            
            Move(destination);
            yield return null;
        }
        _sequenceIsRunning = false;
    }

    public void Shake(float amount, float time) => StartCoroutine(Shaker(amount, time));
    private IEnumerator Shaker(float amount, float time)
    {
        float timer = 0;
        while (time != 0? timer < time : amount > 0.01f)
        {
            transform.position += new Vector3(Random.Range(-amount, amount), Random.Range(-amount, amount),0);
            yield return new WaitForSeconds(0.04f);
            amount *= 0.8f;
            timer += 0.04f;
        }
    }
}
