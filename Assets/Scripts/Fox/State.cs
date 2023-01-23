public abstract class State
{
    protected Fox fox;
    protected StateMachine stateMachine;

    protected State(Fox fox, StateMachine stateMachine)
    {
        this.fox = fox;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void LogicUpdate() { }

    public virtual void Exit() { }
}