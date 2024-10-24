using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultState : PlayerBaseState
{
    public PlayerDefaultState(Player player) : base(player) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter::::PlayerDefaultState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit::::PlayerDefaultState");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Update::::PlayerDefaultState");
    }
}
