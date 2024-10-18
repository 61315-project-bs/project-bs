using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일단 지금은 Boost로만 한정 추후 추상화 예정
public class PlayerUseSkillState : PlayerBaseState
{
    private IEnumerator IE_OnUseSkillHandle = null;
    public PlayerUseSkillState(Player player) : base(player) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter::::PlayerMoveState");
        OnUseSkill();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit::::PlayerMoveState");
        _player.Animator.SetBool("isUseSkill", false);
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnUseSkill()
    {
        if (IE_OnUseSkillHandle != null) return;

        _player.StartCoroutine(IE_OnUseSkillHandle = IE_OnUseSkill());
    }

    private IEnumerator IE_OnUseSkill()
    {
        Boost boost = _player.TrainerData.UltimateSkill;
        _player.Animator.SetBool("isUseSkill", true);
        _player.IsSkillCooltime = true;
        yield return IE_WaitTime();
        yield return IE_SkillEffect(boost);
        yield return IE_Cooltime(boost);
    }
    private IEnumerator IE_WaitTime()
    {
        float currTime = 0.0f;
        float maxTime = 1.0f;
        while (currTime < maxTime)
        {
            currTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"IE_WaitTime");
        _player.Animator.SetBool("isUseSkill", false);
        _player.FsmRunner.CurrentState = _player.PlayerStateHandler.IdleState;
    }

    private IEnumerator IE_SkillEffect(Boost boost)
    {
        _player.Modifier.MoveSpeed = boost.BoostSpeed;
        float currTime = 0.0f;
        float maxTime = boost.Duration;
        while (currTime < maxTime)
        {
            currTime += Time.deltaTime;
            yield return null;
        }
        _player.Modifier.MoveSpeed = 1.0f;
    }
    public IEnumerator IE_Cooltime(Boost boost)
    {
        float currTime = 0.0f;
        float maxTime = boost.CoolTime;
        while (currTime < maxTime)
        {
            currTime += Time.deltaTime;
            yield return null;
        }
        _player.IsSkillCooltime = false;
        IE_OnUseSkillHandle = null;
    }
}
