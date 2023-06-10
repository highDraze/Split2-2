using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Mover mover;
    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mover.SetInputVector(context.ReadValue<Vector2>());
    }
}
