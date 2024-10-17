

public abstract class PlayerBaseState : IState
{
    protected Player _player;

    protected PlayerBaseState(Player player)
    {
        _player = player;

    }


    public virtual void Enter()
    {
    }

    public virtual void Exit()
    { 
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update() {}

}
