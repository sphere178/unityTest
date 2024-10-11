using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : CharacterCombatSystemBase
{
    private PlayerInputSet _playerInputSet;
    public bool CanAttackInput;
    

   

    protected override void Awake()
    {
        base.Awake();
        _playerInputSet = GetComponentInParent<PlayerInputSet>();
       
        
       
        
        
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        Defen();
        Attack();
    }

    private bool CanAttack()
    {
        if(CanAttackInput)
        return true;
        else
        {
            return false;
        }
    }
    private void Attack()
    {
        
        
        if (CanAttack() && _playerInputSet.LAtkInput)
        {
            animator.SetTrigger(LAtkID);
        }
        else
        {
            animator.ResetTrigger(LAtkID);
        }
    }

    private bool CanDefen()
    {
        if (animator.CheckAnimationTag("Attack") || animator.CheckAnimationTag("Hit"))
            return false;

        return true;
    }
    private void Defen()
    {
        if (CanDefen())
        {
           
            animator.SetBool(DefenID,_playerInputSet.DefenInput);
        }
        else
        {
            animator.SetBool(DefenID,false);
        }
    }
    //¹¥»÷¼ì²â
    
    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
    

   // private void OnDrawGizmos()
    //{
       // Gizmos.color = Color.white;
       // Gizmos.DrawSphere(attackDetectCenter.position, attackDetectRang);
  //  }

}
