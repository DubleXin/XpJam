using UnityEngine;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour, IActuator
{
    [SerializeField] private string _levelName;
    [SerializeField] private bool _irl;
    public bool Used { get => _used; }
    private bool _used = false;
    private bool _wasLoaded = false;
    [System.Obsolete]
    void IActuator.Action()
    {
        if (_used)
            return;
        bool[] terminalData = MakeTerminalsArray();
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if(_irl)
            DataTransferer.UpdateData(DataTransferer.GeneralPreviousLevelName, new object[] {player.transform.position, terminalData });
        var load = SceneManager.LoadSceneAsync(_levelName, LoadSceneMode.Single);
        if (load.isDone == true)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_levelName));
        _used = true;
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
            //result[int.Parse(args[1])] = actuators[i].GetComponent<Terminal>().Used;
        }
        return result;
    }
}
