using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParentController : MonoBehaviour
{
    public StateController state;
    public BodyController body = new();
    public float fps;
    public float animationSpeed;
    private Vector2 moveInput;
    public PlayerController swordController;
        

    void Awake()
    {
        state = new();
        Time.fixedDeltaTime = 1 / fps;
        swordController = GameObject.FindGameObjectWithTag("sword").GetComponent<PlayerController>();

    }

    void FixedUpdate()
    {
        if(!swordController.isFiring)
        {
            body.BodyMovement(moveInput);
        }
    }

    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

}
