using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Player : MonoBehaviour
{
    [SerializeField] private GunController _gunController;
    [SerializeField] private TrainerData _trainerData;
    private PlayerStateHandler _playerStateHandler;
    public PlayerBaseData PlayerBaseData { get; private set; } = new PlayerBaseData();
    public TrainerData TrainerData { get { return _trainerData; } }
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
