using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class PlayerInputSet : MonoBehaviour
{
    private InputControl _inputController;

    public bool LAtkInput
    {
        get => _inputController.PlayerInput.Attack.triggered;
    }
    public bool RunInput
    {
        get => _inputController.PlayerInput.Run.phase==InputActionPhase.Performed;
    }
    public bool DefenInput
    {
        get => _inputController.PlayerInput.Defen.phase == InputActionPhase.Performed;
    }

    public Vector2 moveInput
    {
        get => _inputController.PlayerInput.Move.ReadValue<Vector2>();
    }

    public Vector2 cameraInput
    {
        get => _inputController.PlayerInput.Camera.ReadValue<Vector2>();
    }

    private void Awake()
    {
        if (_inputController == null)
            _inputController = new InputControl();
    }

    private void OnEnable()
    {
        _inputController.Enable();
    }

    private void OnDisable()
    {
        _inputController.Disable();
    }
}
