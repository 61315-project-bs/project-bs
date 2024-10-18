using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseState : PlayerBaseState
{
    public PlayerPauseState(Player player) : base(player) {}
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter::::PlayerPauseState");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit::::PlayerPauseState");
    }

    public override void Update() { }
}
