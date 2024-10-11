using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealthSystemBase
{
    private bool CanExecute;
    protected PlayerInputSet _playerInputSet;
    [SerializeField]private float CanExecuteTime;
    protected override void Awake()
    {
        base.Awake();
        _playerInputSet = GetComponent<PlayerInputSet>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        DetectExecuteInput();
    }

    
    public override void HitEvent(string hitAnimName, Transform attacker)
    {
        if (animator.CheckAnimationTag("Defen"))
        {
            
            Parry(hitAnimName,attacker);
        }
        if(!animator.CheckAnimationTag("Defen"))
        {

            animator.Play(hitAnimName, 0, 0);
            PlayHitSound();
        }
    }

    private void Parry(string hitAnimName, Transform attacker)
    {
        switch (hitAnimName)
        {
            case "Hit_F":animator.Play("Parry_Up_L_R"); PlayNormalParrySound(); break;
            case "Hit_Up_L_R": animator.Play("Parry_Up_L_R"); PlayNormalParrySound(); break;
            case "Hit_Up_R_L": animator.Play("Parry_Up_R_L"); PlayNormalParrySound(); break;
            case "Hit_D_R_L": animator.Play("Parry_D_R_L"); PlayNormalParrySound(); break;
            case "Hit_D_L_R": animator.Play("Parry_D_L_R"); PlayNormalParrySound(); break;
            case "Hit_Spine": animator.Play("Parry_Up_F"); ExecuteEvent(attacker); break;
        }
        
    }

    private void ExecuteEvent(Transform attacker)
    {
        PlayExecuteParrySound();
        Time.timeScale = 0.5f;
        CanExecute = true;
        attacker.GetComponentInChildren<Animator>().Play("Parry_Rebound", 0, 0);
        GameObjectPoolSystem.instance.TakeAvailableTimer().InitTimer(() => { CanExecute = false;Time.timeScale = 1;/*animator.Play("Idle", 0, 0)*/; }, CanExecuteTime);
        

    }
    private void DetectExecuteInput()
    {
        if (CanExecute && _playerInputSet.LAtkInput)
        {
            animator.Play("ExecuteAttack", 0, 0);
            CanExecute = false;
        }
    }

}
