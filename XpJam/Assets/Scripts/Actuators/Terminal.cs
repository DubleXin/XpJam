using UnityEngine;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour, IActuator
{
    [SerializeField] private string _levelName;
    private bool _used;
    public bool Used { get => _used; }
    private bool _wasLoaded = false;
    [System.Obsolete]
    void IActuator.Action()
    {
        if (_used)
            return;
        _used = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (StateMachine.LevelStatus == StateMachine.LevelStage.ON_OVERWORLD)
        {
            bool[] terminalData = MakeTerminalsArray();
            DataTransferer.UpdateData(DataTransferer.GeneralPreviousLevelName, new object[] { player.transform.position, terminalData });
        }
        var load = SceneManager.LoadSceneAsync(_levelName, LoadSceneMode.Single);
        if (load.isDone == true)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_levelName));
    }
    public void LoadParams(bool used)
    {
        if (_wasLoaded)
            return;
        _wasLoaded = true;
        _used = used;
    }
    private bool[] MakeTerminalsArray()
    {
        GameObject[] actuators = GameObject.FindGameObjectsWithTag("Actuator");
        bool[] result = new bool[actuators.Length];
        for (int i = 0; i < result.Length; i++)
        {
            string[] args = actuators[i].name.Split('_');
            result[int.Parse(args[1])] = actuators[i].GetComponent<Terminal>().Used;
        }
        return result;
    }
}
