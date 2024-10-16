using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{
    public ReactiveProperty<Vector2> MoveDir { get; private set; } = new ReactiveProperty<Vector2>();

    public void OnMove(InputAction.CallbackContext callback)
    {
        MoveDir.Value = callback.ReadValue<Vector2>();
    }

}
