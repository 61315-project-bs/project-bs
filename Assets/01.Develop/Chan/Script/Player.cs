using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Player : MonoBehaviour
{
    private PlayerStateHandler _playerStateHandler;
    
    public Animator Animator { get; private set; }
    public FsmRunner FsmRunner { get; private set; }
    public PlayerInputController InputController { get; private set; }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        FsmRunner = new FsmRunner();
        _playerStateHandler = new PlayerStateHandler(this);
        Animator = GetComponent<Animator>();
        InitInputController();
    }

    private void InitInputController()
    {
        InputController = GetComponent<PlayerInputController>();
        InputController.MoveDir
            .AsObservable()
            .Subscribe(dir =>
            {
                if (dir == Vector2.zero) FsmRunner.CurrentState = _playerStateHandler.IdleState;
                else FsmRunner.CurrentState = _playerStateHandler.MoveState;
            });
    }
    private void OnDisable()
    {
        FsmRunner.Dispose();
    }
}
