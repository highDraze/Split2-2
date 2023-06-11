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
    Rigidbody rb;

    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;
    Vector3 refVel = Vector3.zero;

    private void Awake()
    {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        //Debug.Log(moveDirection);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        Debug.Log(moveDirection);
        moveDirection *= MoveSpeed;

        //controller.Move(moveDirection * Time.deltaTime);

        
        float smoothVal = .1f; // Higher = 'Smoother'  
        rb.velocity = Vector3.SmoothDamp(rb.velocity, moveDirection, ref refVel, smoothVal);
        //Debug.Log(rb.velocity);
    }
}
