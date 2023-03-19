using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Collider2D _collider;
    private float _damage;
    private string _mask;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        transform.localPosition = Vector3.zero;
    }
    public void SetParams(float damage,string mask) 
    { 
        _damage = damage;
        _mask = mask;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colision");
        if (collision.transform.CompareTag(_mask))
        {
            Debug.Log("mask");
            if (collision.gameObject.TryGetComponent(out DamageReceiver receiver))
            {
                Debug.Log("catch");
                receiver.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
