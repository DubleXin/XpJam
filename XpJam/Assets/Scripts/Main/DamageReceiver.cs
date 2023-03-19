using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [Header("Main Stats")]
    [SerializeField] private Stat _hp;

    public delegate bool OnTakingDamageHandler(out float damage);
    public event OnTakingDamageHandler OnTakingDamage;
    public delegate bool OnTakingDamageFromSourceHandler(out float damage, GameObject source);
    public event OnTakingDamageFromSourceHandler OnTakingFromSourceDamage;
    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    public void SetStats(Stat hp)
    {
        if(_hp is null)
            _hp = hp;
    }
    public void SetStats(float hpAmount)
    {
        if (_hp is null)
            _hp = new(hpAmount);
    }
    public void TakeDamage(float damage, GameObject source)
    {
        if (_hp.FinalValue <= 0)
        {
            OnDeath?.Invoke();
            return;
        }
        _hp.UpdateBaseValue(-1 * damage);
        if(OnTakingDamage != null)
            OnTakingDamage(out damage);
        if(source != null && OnTakingFromSourceDamage != null)
            OnTakingFromSourceDamage(out damage, source);
    }   
    public void TakeDamage(float damage) => TakeDamage(damage, null);
}
