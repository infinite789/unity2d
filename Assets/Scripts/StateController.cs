using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController
{

    public Orientation currentOrientation;
    public bool isLeftInitially = true;

    public StateController()
    {
        int id = Random.Range(1, 5);
        currentOrientation = (Orientation)id;
    }


    public Orientation getOrientation(Vector2 moveInput)
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
}

