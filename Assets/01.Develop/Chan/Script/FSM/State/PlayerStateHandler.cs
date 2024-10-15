using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler
{
    public PlayerDefaultState _defaultState { get; private set; }
    public PlayerIdleState _idleState { get; private set; }
    public PlayerMoveState _moveState {get; private set; }

    public PlayerStateHandler(Player player)
    {
        _defaultState = new PlayerDefaultState(player);
        _idleState = new PlayerIdleState(player);
        _moveState = new PlayerMoveState(player);
    }
}
