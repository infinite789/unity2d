using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLayerController : MonoBehaviour
{
    public CharacterController parentController;
    public AnimationController anim = new();
    private Vector2 moveInput;
    public float hitTimer = 0f;
    public float hitDuration = 1f;
    public GameObject player;
    private CharacterLayerController playerController;
    public CharacterLayerController[] layerControllers;
    private StateController stateController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<CharacterLayerController>();
        layerControllers = player.GetComponentsInChildren<CharacterLayerController>();
        stateController = GameObject.FindObjectOfType<StateController>();
        anim.CreateAnimations(gameObject);

        if (stateController.getOrientation() == Orientation.right)
        {
            anim.renderer.flipX = true;
        } else
        {
            anim.renderer.flipX = false;
        }

        if (this.tag == "sword")
        {
            gameObject.SetActive(stateController.hasSword);
        }
    }

    private void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(hitTimer);
        //Debug.Log(stateController.isFiring);
        //Debug.Log(anim.config[0].animationClass);
        anim.UpdateAnimation(moveInput);

        if(stateController.isFiring && hitTimer > 0 && this.tag == "sword")
        {
            hitTimer -= Time.fixedDeltaTime;
        }
        //Debug.Log(hitTimer);

        if(stateController.isFiring && hitTimer <= 0 && this.tag == "sword")
        {
            stateController.isFiring = false;
            Debug.Log("hello my friend");
            string animationString = stateController.getCurrentClass() + "_" + stateController.getCurrentOrientationString();
            if (animationString.Contains("sword") && !animationString.Contains("up"))
            {
                SetZPos(-0f);
            }

        }

     
    }
    

    private void OnFire(InputValue input)
    {
        if(stateController.hasSword && this.tag == "sword")
        {
            if (!stateController.isFiring && moveInput == Vector2.zero)
            {
                hitTimer = hitDuration;
                stateController.isFiring = true;

                for (int i = 0; i < layerControllers.Length; i++)
                {
                    layerControllers[i].anim.animationCounter = 0;
                    layerControllers[i].anim.spriteId = 0;
                }
            }

            string animationString = stateController.getCurrentClass() + "_" + stateController.getCurrentOrientationString();
            if (animationString.Equals("hit_sword_up"))
            {
                SetZPos(-1f);

            }
            else if (animationString.Contains("sword"))
            {
                SetZPos(-0.5f);
            }

            Debug.Log("hii");
            Debug.Log(hitTimer);
            Debug.Log(stateController.isFiring);

        }
    }
    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        flipXOnMove();

        string animationString = anim.currentAnimation + "_" + stateController.getCurrentOrientationString();
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
        if (stateController.isLeftInitially)
        {
            if (stateController.getOrientation() == Orientation.left)
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
            if (stateController.getOrientation() == Orientation.left)
            {
                anim.renderer.flipX = true;
            }
            else
            {
                anim.renderer.flipX = false;
            }
        }

        if (stateController.getOrientation() == Orientation.up || stateController.getOrientation() == Orientation.down)
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
