using UnityEngine;

public class InputSubscriber : MonoBehaviour
{
    /*private InputHandler _inputHandler;

    #region PlayerLinks
    private GameObject Player = GameObject.FindGameObjectWithTag("Player");
    #endregion

    #region Init
    private void Awake()
    {
        _inputHandler = new InputHandler();
        SubscribeNecessaries();
        SubscribeCharacterDependant();
    }
    #endregion
    #region Subscriptions
    private void SubscribeNecessaries()
    {
    }
    private void SubscribeCharacterDependant()
    {
        #region Movement
        _inputHandler.Player.XDirection.started += _ => _movementHandler.Direction = new Vector2(_.ReadValue<float>(), _movementHandler.Direction.y);
        _inputHandler.Player.XDirection.canceled += _ => _movementHandler.Direction = new Vector2(0, _movementHandler.Direction.y);

        _inputHandler.Player.YDirection.started += _ => _movementHandler.Direction = new Vector2(_movementHandler.Direction.x, _.ReadValue<float>());
        _inputHandler.Player.YDirection.canceled += _ => _movementHandler.Direction = new Vector2(_movementHandler.Direction.x, 0);

        _inputHandler.Player.YDirection.started += _ => _movementHandler.VerticalMove(_.ReadValue<float>());
        _inputHandler.Player.YDirection.canceled += _ => _movementHandler.VerticalMove(0);
        #endregion
    }
    public void UnsubscribeCharacterDependant()
    {
        #region Movement
        _inputHandler.Player.XDirection.started -= _ => _movementHandler.Direction = new Vector2(_.ReadValue<float>(), _movementHandler.Direction.y);
        _inputHandler.Player.XDirection.canceled -= _ => _movementHandler.Direction = new Vector2(0, _movementHandler.Direction.y);

        _inputHandler.Player.YDirection.started -= _ => _movementHandler.Direction = new Vector2(_movementHandler.Direction.x, _.ReadValue<float>());
        _inputHandler.Player.YDirection.canceled -= _ => _movementHandler.Direction = new Vector2(_movementHandler.Direction.x, 0);

        _inputHandler.Player.YDirection.started -= _ => _movementHandler.VerticalMove(_.ReadValue<float>());
        _inputHandler.Player.YDirection.canceled -= _ => _movementHandler.VerticalMove(0);

        _inputHandler.Player.MainSkill.started -= _ => _movementHandler.UseSkill((byte)PlayerSkillID.MAIN_SKILL);
        _inputHandler.Player.FirstSkill.started -= _ => _movementHandler.UseSkill((byte)PlayerSkillID.FIRST_SKILL);
        _inputHandler.Player.SecondSkill.started -= _ => _movementHandler.UseSkill((byte)PlayerSkillID.SECOND_SKILL);
        _inputHandler.Player.UltimativeSkill.started -= _ => _movementHandler.UseSkill((byte)PlayerSkillID.ULTIMATIVE_SKILL);
        #endregion
    }
    public void SubscribeDialogue()
    {
        _inputHandler.Player.Interaction.started += _ => _dialogueWriter.ValidateNextAction();
        _inputHandler.Player.Shot.started += _ => _dialogueWriter.ValidateNextAction();
    }
    public void UnsubscribeDialogue()
    {
        _inputHandler.Player.Interaction.started -= _ => _dialogueWriter.ValidateNextAction();
        _inputHandler.Player.Shot.started -= _ => _dialogueWriter.ValidateNextAction();
    }
    #endregion

    #region Enable/Disable
    private void OnEnable()
    {
        _inputHandler.Player.XDirection.Enable();
        _inputHandler.Player.YDirection.Enable();
        _inputHandler.Player.MainSkill.Enable();
        _inputHandler.Player.FirstSkill.Enable();
        _inputHandler.Player.SecondSkill.Enable();
        _inputHandler.Player.UltimativeSkill.Enable();
        _inputHandler.Player.Menu.Enable();
        _inputHandler.Player.Esc.Enable();
        _inputHandler.Player.AlternativeChoose.Enable();
        _inputHandler.Player.FirstSlot.Enable();
        _inputHandler.Player.SecondSlot.Enable();
        _inputHandler.Player.ThirdSlot.Enable();
    }
    private void OnDisable()
    {
        _inputHandler.Player.XDirection.Disable();
        _inputHandler.Player.YDirection.Disable();
        _inputHandler.Player.MainSkill.Disable();
        _inputHandler.Player.FirstSkill.Disable();
        _inputHandler.Player.SecondSkill.Disable();
        _inputHandler.Player.UltimativeSkill.Disable();
        _inputHandler.Player.Menu.Disable();
        _inputHandler.Player.Esc.Disable();
        _inputHandler.Player.AlternativeChoose.Disable();
        _inputHandler.Player.FirstSlot.Disable();
        _inputHandler.Player.SecondSlot.Disable();
        _inputHandler.Player.ThirdSlot.Disable();
    }
    #endregion
    */
}
