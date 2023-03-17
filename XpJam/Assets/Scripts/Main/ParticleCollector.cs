using System.Collections;
using UnityEngine;

public class ParticleCollector : MonoBehaviour
{
    [SerializeField] private float TimeToCollect;
    private void Start() => StartCoroutine(Collect());

    private IEnumerator Collect()
    {
        yield return new WaitForSeconds(TimeToCollect);
        Destroy(gameObject);
    }
}
