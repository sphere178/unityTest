using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : CharacterMovememtBase
{
    private PlayerInputSet _playerInputSet;
    private Transform CameraTransform;
    
    
    protected override void Awake()
    {
        base.Awake();
        _playerInputSet = GetComponent<PlayerInputSet>();
        CameraTransform = Camera.main.transform;
       

    }


    private void Start()
    {
        InitAnimation("Idle");
    }

    

    private void Update()
    {
        UpdateMoveAniamtion();
        PlayerRotate();
        PlayerMoveBaseOnCamera(moveSpeed*RunSpeedMult);
        UpdateAnimationMove(GetPlayerMoveDirection());
    }

    
    #region ÒÆ¶¯
    private void PlayerMoveBaseOnCamera(float _movespeed)
    {
        if (!CanMove()) return;
        if (CanMove())
        {
            Vector3 _movedirection = GetPlayerMoveDirection();
            CharacterMove(_movedirection, _movespeed);
        }
    }

    private Vector3 GetPlayerMoveDirection()
    {
        Vector3 _movedirection = CameraTransform.forward * _playerInputSet.moveInput.y + CameraTransform.right * _playerInputSet.moveInput.x;
        _movedirection.y = 0;
        
        return _movedirection.normalized;
    }

    
    private void PlayerRotate()
    {
        Vector3 _movedirction = GetPlayerMoveDirection();
        if (CanRotate())
        {

            // transform.forward =Vector3.SmoothDamp( transform.forward,_movedirction,ref rotateSpeed,rotateTime);
            transform.forward = Vector3.Slerp(transform.forward, _movedirction, rotateTime);
        }
    }

    private bool CanMove()
    {
        if (animator.CheckAnimationName("Run_End")|| animator.CheckAnimationTag("Attack") || animator.CheckAnimationTag("Hit") || animator.CheckAnimationTag("Defen") || animator.CheckAnimationTag("ExecuteAttack"))
        {
            return false;
        }
            return true;
    }

    private bool CanRotate()
    {
        if (animator.CheckAnimationName("Run_End") || animator.CheckAnimationTag("Attack") || animator.CheckAnimationTag("Hit") || animator.CheckAnimationTag("Defen") || animator.CheckAnimationTag("ExecuteAttack"))
        {
            return false;
        }
        return true;
    }

    private void UpdateMoveAniamtion()
    {
        
        if (_playerInputSet.moveInput.magnitude > 0)
        {
            if (_playerInputSet.RunInput)
            {
                animator.SetBool(RunID, true);
                RunSpeedMult = 2f;
                return;
            }
            else
            {
                animator.SetBool(RunID, false);
                RunSpeedMult = 1f;
            }
            animator.SetBool(WalkID, true);

        }
        else
        {
            RunSpeedMult = 1f;
            animator.SetBool(RunID, false);
            animator.SetBool(WalkID, false);
        }
    }

    

    #endregion
}
