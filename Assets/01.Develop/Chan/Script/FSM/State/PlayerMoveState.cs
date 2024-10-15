using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(Player player) : base(player) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter::::PlayerMoveState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit::::PlayerMoveState");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Update::::PlayerMoveState");
    }
}
