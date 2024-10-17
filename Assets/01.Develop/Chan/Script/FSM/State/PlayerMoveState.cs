using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(Player player) : base(player) {}
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter::::PlayerMoveState");
        _player.Animator.SetBool("isMove", true);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit::::PlayerMoveState");
        _player.Animator.SetBool("isMove", false);
    }

    public override void Update()
    {
        base.Update();
        Vector3 moveDir = new Vector3(_player.InputController.MoveDir.Value.x, 0, _player.InputController.MoveDir.Value.y);
        _player.transform.position += moveDir * _player.Temp_PlayerData.MoveSpeed * Time.deltaTime; // 실제 이동 처리
    }
}
