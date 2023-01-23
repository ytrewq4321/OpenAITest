public class RunState : State
{
    public RunState(Fox fox, StateMachine stateMachine) : base(fox, stateMachine) { }

    public override void Enter()
    {
        fox.SetAnimationBool("Run", true);
        fox.ResumeMove();
    }

    public override void LogicUpdate()
    {
        fox.SetDestination();
        if (fox.IsInteractWithUser)
            stateMachine.ChangeState(fox.UserInteraction);
    }

    public override void Exit()
    {
        fox.SetAnimationBool("Run", false);
        
    }
}
