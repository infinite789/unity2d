using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public BodyController body = new();
    public float fps;
    private Vector2 moveInput;
    public StateController stateController;
        

    void Awake()
    {

        Time.fixedDeltaTime = 1 / fps;
        stateController = GameObject.FindObjectOfType<StateController>();
    }

    void FixedUpdate()
    {
        if(!stateController.isFiring)
        {
            body.BodyMovement(moveInput);
        }
    }

    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

}
