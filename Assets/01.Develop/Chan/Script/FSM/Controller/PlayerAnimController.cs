using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController
{
    private Animator _animator;
    public PlayerAnimController(Animator animator)
    {
        _animator = animator;
    }

    public virtual void StartAnimation(int animatorHash)
    {
        _animator.SetBool(animatorHash, true);
    }

    public virtual void StopAnimation(int animatorHash)
    {
        _animator.SetBool(animatorHash, false);
    }

    public virtual void SetTriggerAnimation(int animatorHash)
    {
        _animator.SetTrigger(animatorHash);
    }

    public virtual void SetFloatAnimation(int animatorHash, float value)
    {
        _animator.SetFloat(animatorHash, value);
    }
    public virtual float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
