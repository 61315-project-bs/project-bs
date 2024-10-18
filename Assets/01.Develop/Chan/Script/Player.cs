using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

// 스킬 사용을 위한 Modifier
public class Temp_PlayerModifier
{
    public float MoveSpeed = 1.0f;
}


public class Player : MonoBehaviour
{
    [SerializeField] private GunController _gunController;
    [SerializeField] private TrainerData<Pistol, Boost> _trainerData;
    public PlayerStateHandler PlayerStateHandler { get; private set; }
    public PlayerBaseData PlayerBaseData { get; private set; } = new PlayerBaseData();
    public TrainerData<Pistol, Boost> TrainerData { get { return _trainerData; } }
    public Temp_PlayerModifier Modifier { get; private set; } = new Temp_PlayerModifier();
    public GunController GunController { get { return _gunController; } }
    public Animator Animator { get; private set; }
    public FsmRunner FsmRunner { get; private set; }
    public PlayerInputController InputController { get; private set; }

    public bool IsSkillCooltime = false;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        FsmRunner = new FsmRunner();
        PlayerStateHandler = new PlayerStateHandler(this);
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
            .Where(_ => FsmRunner.CurrentState != PlayerStateHandler.UseSkillState)
            .Subscribe(dir =>
            {
                if (dir == Vector2.zero) FsmRunner.CurrentState = PlayerStateHandler.IdleState;
                else FsmRunner.CurrentState = PlayerStateHandler.MoveState;
            }).AddTo(gameObject);
        InputController.Act_UseSkill += () =>
        {
            if (IsSkillCooltime)
                return;
            FsmRunner.CurrentState = PlayerStateHandler.UseSkillState;

        };
    }
    private void OnDisable()
    {
        FsmRunner.Dispose();
    }
}
