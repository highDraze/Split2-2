using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Mover : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 3f;

    [SerializeField]
    private int playerIndex = 0;

    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(InputAction.CallbackContext context)
    {
        Debug.Log("InputVector");
        inputVector = context.ReadValue<Vector2>();
    }

    void Update()
    {
        Debug.Log("Update");
        Debug.Log(inputVector);
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= MoveSpeed;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
