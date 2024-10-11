using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SoundController;
using static UnityEngine.GraphicsBuffer;

public class CharacterCombatSystemBase : MonoBehaviour
{
    [Header("音效")]
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected SoundController soundController;
    
    [Header("动画")]
    protected Animator animator;
    protected int LAtkID;
    protected int DefenID;
    
    [Header("攻击检测")]
    [SerializeField] protected float attackDetectRang;
    [SerializeField] protected Transform attackDetectCenter;
    [SerializeField] protected Collider[] attackedTargets;
    [SerializeField] protected int MaxattackedTargetNumber;
   
    [SerializeField] protected LayerMask whatIsEnemy;
    [Header("敌人探测")]
    [SerializeField] protected Transform detectEnemyCenter;
    [SerializeField] protected float detectEnemyRang;
    [SerializeField] protected Collider[] detectedEnemy;
    [SerializeField] protected int MaxDetectedEnemyNumber;
    [SerializeField] protected float DetectAngle;
    [SerializeField] protected Transform currentTargetEnemy;

    [SerializeField] protected CharacterMovememtBase characterMovement;


    protected virtual void Awake()
    {
        detectedEnemy = new Collider[MaxDetectedEnemyNumber];
        attackedTargets = new Collider[MaxattackedTargetNumber];
        audioSource = GetComponentInParent<AudioSource>();
        animator = GetComponent<Animator>();
        characterMovement = GetComponentInParent<CharacterMovememtBase>();
        LAtkID = Animator.StringToHash("LAtk");
        DefenID = Animator.StringToHash("Defen");
    }
    void Start()
    {
        
    }

    
     void Update()
    {
                      
    }
    public void OnAnimationEvent(string hitAnimName)
    {
        PlayWeaponSound();

        Physics.OverlapSphereNonAlloc(attackDetectCenter.position, attackDetectRang, attackedTargets, whatIsEnemy);
        for (int i = 0; i < attackedTargets.Length; i++)
        {
            if (attackedTargets[i] != null)
            {
                if (attackedTargets[i].TryGetComponent<IDamager>(out var damager))
                {
                    
                    damager.HitEvent(hitAnimName,transform.root);


                }
                attackedTargets[i] = null;
            }

        }


    }

    protected void PlayWeaponSound()
    {
        soundController.PlaySound(audioSource, SoundType.attack);
    }

    protected void DetectEnemy() //检测所有潜在的敌人
    {
        
        
       int enemyCount= Physics.OverlapSphereNonAlloc(detectEnemyCenter.position,detectEnemyRang,detectedEnemy,whatIsEnemy);
        if (enemyCount == 0)
        {
            Array.Clear(detectedEnemy,0,detectedEnemy.Length);
        }

    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackDetectCenter.position, attackDetectRang);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(detectEnemyCenter.position, detectEnemyRang);
    }

    protected void FindCurrentTargetEnemy() //选择一个敌人作为当前目标
    {
        
        foreach(var enemy in detectedEnemy)
        {
            if (enemy == null)
            {
                continue;
            }
            Vector3 rootToEnemy = enemy.transform.position - transform.root.position;
           float dotValue=Vector3.Dot(rootToEnemy, transform.root.forward);
            float distanceMulValue = rootToEnemy.magnitude; /* transform.root.forward.magnitude=1;*/
            float cosValue = dotValue / distanceMulValue;
            if (cosValue >= Mathf.Cos(DetectAngle))
            {
                currentTargetEnemy = enemy.transform;
                return;
            }
            
        }
        Debug.Log("resettargetenemy");
        currentTargetEnemy = null;
    }

    protected void LockOnCurrentTargetEnemy()
    {
        if (currentTargetEnemy == null) return;
       Quaternion targetRotation= Quaternion.LookRotation(currentTargetEnemy.position-transform.root.position);
        transform.root.rotation = Quaternion.Slerp(transform.root.rotation, targetRotation, 0.2f);
    }
}
