using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public BodyController body = new();
    public float fps;
    public float animationSpeed;
    private Vector2 moveInput;
    public CharacterLayerController swordController;
        

    void Awake()
    {
        Time.fixedDeltaTime = 1 / fps;
        swordController = GameObject.FindGameObjectWithTag("sword").GetComponent<CharacterLayerController>();

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
