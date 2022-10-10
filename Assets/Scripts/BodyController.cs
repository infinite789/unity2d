using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyController 
{
    public Rigidbody2D rbody;
    public float speed;
    public float collisionOffset;

    private ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    /* 
     * Update  
     */
    public bool BodyMovement(Vector2 direction)
    {

        bool isColliding = false;

        // if movement input is not 0, try to move
        if (direction != Vector2.zero)
        {
            isColliding = TryMove(direction);

            if (!isColliding)
            {
                isColliding = TryMove(new Vector2(direction.x, 0));

                if (!isColliding)
                {
                    isColliding = TryMove(new Vector2(0, direction.y));
                }
            }
        } else
        {
            rbody.MovePosition(rbody.position);
        }

        return isColliding;
    }

    /*
     * Collision check
     */
    public bool TryMove(Vector2 direction)
    {
        direction.y *= 0.7f;
        int count = rbody.Cast(
              direction,
              movementFilter,
              castCollisions,
              this.speed * Time.fixedDeltaTime + collisionOffset);

            rbody.MovePosition(rbody.position + this.speed * Time.fixedDeltaTime * direction);
            return true;
      
    }

}

