using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitExpandFunction 
{
    public static bool CheckAnimationName(this Animator animator,string animationName)
    {
       return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public static bool CheckAnimationTag(this Animator animator, string animationTag)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag(animationTag);
    }
}
