using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private PlayerInputSet _playerinputSet;
    private Transform CameraTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float AngleX;
    [SerializeField] private float AngleY;
    [SerializeField] private float AngleYMin;
    [SerializeField] private float AngleYMax;
    [SerializeField] private float RotateDampTime;
    private Vector3 RotateSpeed;
    [SerializeField] private float MouseSensitive;
    private Vector3 currentRotation;
    [SerializeField] private float SphereRadius; //µ½Íæ¼ÒµÄ¾àÀë
    [SerializeField] private LayerMask WhatCameraCollision;
    [SerializeField] private float CollisionOffset;
    // private Transform CameraOnPlayerEye;

    private void Awake()
    {
        CameraTransform =Camera.main.transform;
        _playerinputSet = targetTransform.GetComponentInParent<PlayerInputSet>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        LockCursor();
        CameraMove();
        LockTarget();
        CheckCameraCollision();
    }

    private void CameraMove()
    {
        if (CanCameraMove())
        {
            AngleX += _playerinputSet.cameraInput.x*MouseSensitive;
            AngleY -= _playerinputSet.cameraInput.y * MouseSensitive;
            AngleY = Mathf.Clamp(AngleY,AngleYMin,AngleYMax);
            Vector3 cameraRotation = new Vector3(AngleY,AngleX,0);
            currentRotation = Vector3.SmoothDamp(currentRotation,cameraRotation,ref RotateSpeed, RotateDampTime);
            CameraTransform.eulerAngles = currentRotation;

        }
    }

    private void LockTarget()
    {
        if (targetTransform == null) return;

        if (targetTransform != null)
        {
            
            CameraTransform.position = targetTransform.position - CameraTransform.forward * SphereRadius;

        }
    }
    private bool CanCameraMove()
    {
        return true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CheckCameraCollision()
    { Vector3 TargetToCamera = CameraTransform.position - targetTransform.position;
        if (Physics.Raycast(targetTransform.position, TargetToCamera,out var hit ,SphereRadius+0.2f, WhatCameraCollision))
        {
            
            float DistancehitPointToTarget = Vector3.Distance(hit.point,targetTransform.position);
            CameraTransform.position = CameraTransform.position - TargetToCamera.normalized * DistancehitPointToTarget * CollisionOffset;
           
        }
       
    }
}
