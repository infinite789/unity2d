using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateController : MonoBehaviour
{

    public Orientation currentOrientation;
    public bool isLeftInitially = true;
    private Vector2 moveInput;
    public float animationSpeed;
    public bool isFiring = false;
    public bool hasSword = false;

    public void Start()
    {
        int id = Random.Range(1, 5);
        currentOrientation = (Orientation)id;
    }


    public Orientation getOrientation()
    {
        Orientation last;

        if (moveInput.x > 0)
        {
            last = Orientation.right;
        }
        else if (moveInput.x < 0)
        {
            last = Orientation.left;
        }
        else if (moveInput.y > 0)
        {
            last = Orientation.up;
        }
        else if (moveInput.y < 0)
        {
            last = Orientation.down;
        }
        else
        {
            last = currentOrientation;
        }

        currentOrientation = last;
        //Debug.Log(currentOrientation);
        return currentOrientation;
    }

    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public string getCurrentOrientationString()
    {
        string orientation = currentOrientation.ToString();
        if (currentOrientation == Orientation.left && !isLeftInitially)
        {
            orientation = "right";
        }
        else if (currentOrientation == Orientation.right && isLeftInitially)
        {
            orientation = "left";
        }
        return orientation;
    }


    public string getCurrentClass()
    {
        string str = "";

        if (moveInput.x == 0 && moveInput.y == 0)
        {
            str = "idle";
        }

        else
        {
            str = "run";
        }


        if (isFiring)
        {
            str = "hit";
        }

        //Debug.Log(str);

        return str;
      
    }

}

