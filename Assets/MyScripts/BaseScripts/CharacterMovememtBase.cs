using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovememtBase : MonoBehaviour
{
   protected CharacterController _characterController;
    protected Animator animator;
    [SerializeField] protected float moveSpeed=2f;
    [SerializeField] protected Vector3 rotateSpeed;
    [SerializeField] protected float RunSpeedMult=2f;
    [SerializeField] protected float rotateTime=0.12f;
    protected int RunID;
     protected int WalkID;
     protected int AttackID;
     protected int AnimationMoveID;
    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        RunID = Animator.StringToHash("Run");
        WalkID = Animator.StringToHash("Walk");
        AttackID = Animator.StringToHash("Attack");
        AnimationMoveID = Animator.StringToHash("AnimationMove");
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    protected void InitAnimation(string InitAnimName)
    {
        animator.Play(InitAnimName, 0, 0);
    }

    protected void CharacterMove(Vector3 moveDirection, float _movespeed)
    {

        _characterController.Move(moveDirection * _movespeed * Time.deltaTime);

    }

    protected void UpdateAnimationMove(Vector3 _movedirection)
    {
        if (animator.CheckAnimationName("Run_End") || animator.CheckAnimationTag("Attack"))
        {

           
            CharacterMove(_movedirection, moveSpeed * animator.GetFloat(AnimationMoveID));

        }
    }
}
