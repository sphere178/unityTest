using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealthSystemBase
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public override void HitEvent(string hitAnimName, Transform attacker)
    {
        animator.Play(hitAnimName, 0, 0);
        transform.rotation = Quaternion.LookRotation(attacker.position - transform.position);
        PlayHitSound();
    }
}
