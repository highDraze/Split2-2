using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Mover mover;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = GetComponent<Mover>();

    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        //mover.SetInputVector(context.ReadValue<Vector2>());
    }

}