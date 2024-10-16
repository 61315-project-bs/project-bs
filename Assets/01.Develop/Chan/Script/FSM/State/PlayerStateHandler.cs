using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler
{
    public PlayerDefaultState DefaultState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState {get; private set; }

    public PlayerStateHandler(Player player)
    {
        DefaultState = new PlayerDefaultState(player);
        IdleState = new PlayerIdleState(player);
        MoveState = new PlayerMoveState(player);
    }
}
