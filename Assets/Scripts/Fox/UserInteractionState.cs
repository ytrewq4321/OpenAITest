public class UserInteractionState : State
{
    public UserInteractionState(Fox fox, StateMachine stateMachine) : base(fox, stateMachine) { }

    public override void Enter()
    {
        fox.SetAnimationBool("UserInteraction", true);
        fox.StopMove();
        
    }

    public override void LogicUpdate()
    {
        fox.LookAtCamera();
        if (!fox.IsInteractWithUser)
            stateMachine.ChangeState(fox.Run);
    }

    public override void Exit()
    {
        fox.SetAnimationBool("UserInteraction", false);
    }
}
