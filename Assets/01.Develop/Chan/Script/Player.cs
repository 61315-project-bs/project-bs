using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class Player : MonoBehaviour
{
    private FsmRunner _fmsRunner;
    private PlayerStateHandler _playerStateHandler;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _fmsRunner = new FsmRunner();
        _playerStateHandler = new PlayerStateHandler(this);
    }
    private void Update()
    {
        _fmsRunner.CurrentState?.Update();
        if(Input.GetKeyDown(KeyCode.A)) _fmsRunner.CurrentState = _playerStateHandler._idleState;
        if (Input.GetKeyDown(KeyCode.S)) _fmsRunner.CurrentState = _playerStateHandler._moveState;
    }
    private void FixedUpdate()
    {
        _fmsRunner.CurrentState?.PhysicsUpdate();
    }

    private void OnDisable()
    {
        _fmsRunner.Dispose();
    }
}
