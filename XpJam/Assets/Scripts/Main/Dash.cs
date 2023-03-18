using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash params")]
    [SerializeField] private bool _canDash;
    [SerializeField] private bool _isDashing;
    [SerializeField] private float _dashingTime;
    [SerializeField] private float _dashingCoolDown;
    [SerializeField] private float _dashPower; 

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    public delegate void OnParamsChangedHandler(bool value);
    public event OnParamsChangedHandler OnCanDashChanged;
    public event OnParamsChangedHandler OnIsDashingChanged;
    public void PerformDash(bool canDash, bool isDashing,float dashingTime, float dashingCooldown, float dashPower,Vector2 direction, Rigidbody2D rigidbody)
    {
        if (!canDash)
            return;
        _canDash = canDash;
        _isDashing = isDashing;
        _dashingTime = dashingTime;
        _rigidbody = rigidbody;
        _dashingCoolDown = dashingCooldown;
        _direction = direction;
        _dashPower = dashPower;
        StartCoroutine(OnDash());
    }
    public void PerformDash() => StartCoroutine(OnDash());
    private IEnumerator OnDash()
    {
        _canDash = false;
        _isDashing = true;
        OnCanDashChangedState();
        OnIsDashingChangedState();
        _rigidbody.velocity = new Vector2(_direction.x * _dashPower, _direction.y * _dashPower);
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
        OnIsDashingChangedState();
        yield return new WaitForSeconds(_dashingCoolDown);
        _canDash = true;
        OnCanDashChangedState();
    }
    private void OnCanDashChangedState()
    {
        if (OnCanDashChanged != null)
            OnCanDashChanged(_canDash);
    }
    private void OnIsDashingChangedState()
    {
        if (OnIsDashingChanged != null)
            OnIsDashingChanged(_isDashing);
    }
}
