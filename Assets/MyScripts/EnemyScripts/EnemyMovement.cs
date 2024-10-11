using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : CharacterMovememtBase
{
    private EnemyCombat enemyCombat;
    protected override void Awake()
    {
        base.Awake();
        enemyCombat = GetComponentInChildren<EnemyCombat>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        UpdateAnimationMove(transform.forward);
    }

    public void CloseToEnemyAndMeetUseDistance(Transform currentTargetEnemy,float distance,int skillid) //接近敌人至当前技能距离
    {
        if (Vector3.Distance(transform.position, currentTargetEnemy.position) <= distance)
        {
           
            enemyCombat.GetSkillConditionByID(skillid).distanceCondition = true;
            animator.SetBool(WalkID, false);
            return;
       

        }
        else
        {
            animator.SetBool(WalkID, true);
            CharacterMove(transform.forward, moveSpeed);
        }
        
    }

    
}
