public static class StateMachine
{
    public delegate void OnLevelStageChangedHandler(LevelStage stage);
    public static event OnLevelStageChangedHandler OnLevelStageChanged;
    public static LevelStage LevelStatus { get; private set; }
    public enum LevelStage
    {
        ON_OVERWORLD,
        ON_HACKING
    }
    public static void ChangeLevelStage(LevelStage stage)
    {
        LevelStatus = stage;
        if(OnLevelStageChanged != null)
            OnLevelStageChanged(stage);
    }
    public enum EnemyState
    {
        IDLE,
        PATROL,
        PURSUIT,
        ATTACK
    }
}