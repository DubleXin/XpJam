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

        DataTransferer.UpdateData("PrevLevel", SceneManager.GetActiveScene());
        SceneManager.LoadScene(_levelName, LoadSceneMode.Additive);
        _used = true;
    }
}
