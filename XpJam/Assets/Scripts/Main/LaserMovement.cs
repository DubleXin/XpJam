using UnityEngine;

public class LaserMovement : MonoBehaviour, IHackable
{
    [SerializeField] private Vector2 _pointA, _pointB;
    [SerializeField] private float _speed;
    public bool IsTurnedOff { get; set; }
    private bool _direction;
    private float _factor;
    private void FixedUpdate()
    {
        if (IsTurnedOff)
            return;

        if (_direction)
        {
            if (_factor > 0)
                _factor -= _speed;
            else
                _direction = !_direction;
        }
        else
        {
            if (_factor < 1)
                _factor += _speed;
            else
                _direction = !_direction;
        }
        transform.position = Vector2.Lerp(_pointA, _pointB, _factor);
    }
}
