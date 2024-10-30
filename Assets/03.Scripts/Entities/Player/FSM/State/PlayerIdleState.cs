using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Player player) : base(player) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter::::PlayerIdleState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit::::PlayerIdleState");
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("Update::::PlayerIdleState");
    }
}
