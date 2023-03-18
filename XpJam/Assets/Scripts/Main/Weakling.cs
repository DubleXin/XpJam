using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weakling : MonoBehaviour
{
    private bool _isDead = false;
    private void Awake() => DataTransferer.UpdateData("RealLevel", SceneManager.GetActiveScene().name);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && !_isDead)
            StartCoroutine(LevelReset());
    }
    private IEnumerator LevelReset()
    {
        _isDead = true;

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(DataTransferer.Data["RealLevel"], LoadSceneMode.Single);
    }
}
