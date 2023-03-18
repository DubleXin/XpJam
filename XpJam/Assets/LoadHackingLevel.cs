using UnityEngine;
public  class LoadHackingLevel : MonoBehaviour
{
    private void Awake() => StateMachine.LevelStatus = StateMachine.LevelStage.ON_HACKING;
}
