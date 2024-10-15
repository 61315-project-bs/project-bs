using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected Player _player;
    protected PlayerAnimController _playerAnimController;
    protected PlayerMoveController _playerMoveController;
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

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update() { }
    private void ReadMovementInput()
    {
    }
}
