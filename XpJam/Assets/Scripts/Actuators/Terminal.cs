using UnityEngine;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour, IActuator
{
    [SerializeField] private string _levelName;
    private bool _used = false;
    void IActuator.Action()
    {
        if (_used)
            return;

        SceneManager.LoadScene(_levelName, LoadSceneMode.Single);
        _used = true;
    }
}
