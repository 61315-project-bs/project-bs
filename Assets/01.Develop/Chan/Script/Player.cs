using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class Temp_PlayerData
{
    public float MoveSpeed { get; private set; }
    public float AttackSpeed { get; private set; }
    public Temp_PlayerData(float moveSpeed, float attackSpeed)
    {
        MoveSpeed = moveSpeed;
        AttackSpeed = attackSpeed;
    }
}

public class Player : MonoBehaviour
{
    [SerializeField] private GunController _gunController;
    private PlayerStateHandler _playerStateHandler;

    public Temp_PlayerData Temp_PlayerData { get; private set; } = new Temp_PlayerData(2, 0.5f);
    public GunController GunController { get { return _gunController; } }
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
        _gunController.InitGun();
    }

    private void InitInputController()
    {
        InputController = GetComponent<PlayerInputController>();
        InputController.InitInputController();
        InputController.MoveDir
            .AsObservable()
            .Subscribe(dir =>
            {
                if (dir == Vector2.zero) FsmRunner.CurrentState = _playerStateHandler.IdleState;
                else FsmRunner.CurrentState = _playerStateHandler.MoveState;
            }).AddTo(gameObject);

    }
    private void OnDisable()
    {
        FsmRunner.Dispose();
    }
}
