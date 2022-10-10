using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ParentController parentController;
    public AnimationController anim = new();
    private Vector2 moveInput;
    public float hitTimer = 0f;
    public float hitDuration = 1f;
    public bool isFiring = false;
    public bool hasSword = false;
    public GameObject player;
    public PlayerController[] layerControllers;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        layerControllers = player.GetComponentsInChildren<PlayerController>();

        anim.CreateAnimations(gameObject);

        if (parentController.state.getOrientation(Vector2.zero) == Orientation.right)
        {
            anim.renderer.flipX = true;
        } else
        {
            anim.renderer.flipX = false;
        }

        if (this.tag == "sword")
        {
            gameObject.SetActive(hasSword);
        }
    }

    private void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(anim.config[0].animationClass);
        anim.UpdateAnimation(moveInput);

        if(isFiring && hitTimer > 0)
        {
            hitTimer -= Time.fixedDeltaTime;
        }
        //Debug.Log(hitTimer);

        if(isFiring && hitTimer <= 0)
        {
            isFiring = false;

            string animationString = anim.getCurrentAnimationString();
            if (animationString.Contains("sword") && !animationString.Contains("up"))
            {
                SetZPos(-0f);
            }

        }

     
    }
    

    private void OnFire(InputValue input)
    {
        if(hasSword)
        {
            if (!isFiring && moveInput == Vector2.zero)
            {
                hitTimer = hitDuration;
                isFiring = true;

                for (int i = 0; i < layerControllers.Length; i++)
                {
                    layerControllers[i].anim.animationCounter = 0;
                    layerControllers[i].anim.spriteId = 0;
                }
            }

            string animationString = anim.getCurrentAnimationString();

            if (animationString.Equals("hit_sword_up"))
            {
                SetZPos(-1f);

            }
            else if (animationString.Contains("sword"))
            {
                SetZPos(-0.5f);
            }

           
        }
    }
    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        flipXOnMove();

        string animationString = anim.getCurrentAnimationString();
        if (animationString.Contains("sword_up"))
        {
            SetZPos(-0.6f);
        }  
        else if (animationString.Contains("sword"))
        {
            SetZPos(0.15f); 
        }


    }

    public void flipXOnMove()
    {

        // SET LEFT/RIGHT ORIENTATION DEPENDING ON INITIAL SIDE ORIENTATION
        if (parentController.state.isLeftInitially)
        {
            if (parentController.state.getOrientation(moveInput) == Orientation.left)
            {
                anim.renderer.flipX = false;
            }
            else
            {
                anim.renderer.flipX = true;
            }
        }
        else
        {
            if (parentController.state.getOrientation(moveInput) == Orientation.left)
            {
                anim.renderer.flipX = true;
            }
            else
            {
                anim.renderer.flipX = false;
            }
        }

        if (parentController.state.getOrientation(moveInput) == Orientation.up || parentController.state.getOrientation(moveInput) == Orientation.down)
        {
            anim.renderer.flipX = false;
        }
    }

    public void SetZPos(float zPos)
    {
         this.transform.position = new Vector3(
                parentController.transform.position.x, 
                parentController.transform.position.y, 
                parentController.transform.position.z + zPos);
    }

}
