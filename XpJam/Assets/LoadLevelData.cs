using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelData : MonoBehaviour
{
    private GameObject _player;
    private void Awake()
    {
        StateMachine.LevelStatus = StateMachine.LevelStage.ON_OVERWORLD;
        _player = GameObject.FindGameObjectWithTag("Player");
        if(TryGetLevelData(out object[] data))
        {
            foreach(object obj in data)
            {
                if(obj != null)
                {
                    if (obj is Vector3)
                    {
                        _player.transform.position = (Vector3)obj;
                        continue;
                    }
                    else if(obj is bool[])
                    {
                        GameObject[] actuators = GameObject.FindGameObjectsWithTag("Actuator");
                        for (int i = 0; i < actuators.Length; i++)
                        {
                            string[] args = actuators[i].name.Split('_');
                            actuators[i].GetComponent<Terminal>().LoadParams(((bool[])obj)[int.Parse(args[1])]);
                        }
                        continue;
                    }
                    Debug.Log("nothing found");
                }
            }
        }
    }
    private bool TryGetLevelData(out object[] data)
    {
        data = null;
        if (!DataTransferer.Data.ContainsKey(DataTransferer.GeneralPreviousLevelName))
            return false;
        data = DataTransferer.Data[DataTransferer.GeneralPreviousLevelName];
        return true;
    }
}
