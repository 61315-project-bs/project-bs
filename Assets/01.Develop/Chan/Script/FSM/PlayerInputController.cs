using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;
using System;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInput_Test _inputActions;
    public ReactiveProperty<Vector2> MoveDir { get; private set; } = new ReactiveProperty<Vector2>();
    public Action Act_Reload { get; set; }
    public Action Act_UseSkill { get; set; }

    public void InitInputController()
    {
        _inputActions = new PlayerInput_Test();
        _inputActions.Player.Move.Enable();
        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;
        _inputActions.Player.Special.Enable();
        _inputActions.Player.Special.started += OnPressSpecialKey;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMove;
        _inputActions.Player.Move.Disable();
    }

    private void OnMove(InputAction.CallbackContext callback)
    {
        MoveDir.Value = callback.ReadValue<Vector2>();
    }

    private void OnPressSpecialKey(InputAction.CallbackContext callback)
    {
        if (callback.control.name == "r")
            Act_Reload();
        else if (callback.control.name == "q")
            Act_UseSkill();
    }


}
