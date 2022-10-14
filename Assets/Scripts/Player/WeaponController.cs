using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public AnimationController anim = new();
    private Vector2 moveInput;
    public GameObject player;
    private CharacterLayerController playerController;
    private CharacterController parentController;
    public CharacterLayerController[] layerControllers;
    private StateController stateController;
    public string animationString;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<CharacterLayerController>();
        parentController = FindGameObject.InChildWithTag(player, "character").GetComponent<CharacterController>();
        stateController = GameObject.FindObjectOfType<StateController>();
        anim.CreateAnimations(gameObject);

        if (stateController.getOrientation() == Orientation.right)
        {
            anim.renderer.flipX = true;
        }
        else
        {
            anim.renderer.flipX = false;
        }

        gameObject.SetActive(stateController.hasSword);
    }

    private void Update()
    {
        if (stateController.isFiring && stateController.hitTimer <= 0)
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
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(hitTimer);
        //Debug.Log(stateController.isFiring);
        //Debug.Log(anim.config[0].animationClass);
        animationString = stateController.getCurrentClass() + "_" + stateController.getCurrentOrientationString();
        anim.UpdateAnimation(moveInput);

        //Debug.Log(hitTimer);

    }


    private void OnFire(InputValue input)
    {
        if (stateController.hasSword)
        {
            if (!stateController.isFiring && moveInput == Vector2.zero)
            {
                stateController.hitTimer = stateController.hitDuration;
                stateController.isFiring = true;
                Debug.Log("before syncing");

                for (int i = 0; i < layerControllers.Length; i++)
                {
                    Debug.Log("syncing");
                    layerControllers[i].anim.animationCounter = 0;
                    layerControllers[i].anim.spriteId = 0;

                
                }
                anim.animationCounter = 0;
                anim.spriteId = 0;
            }
            Debug.Log(animationString);
            if (animationString.Contains("up"))
            {
                SetZPos(-1f);


            }
            else
            {
                SetZPos(-0.15f);
            }


        }
    }
    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        flipXOnMove();

        if (animationString.Contains("up"))
        {
            SetZPos(-0.6f);
        }
        else
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
        Debug.Log(zPos);
        this.transform.position = new Vector3(
               parentController.transform.position.x,
               parentController.transform.position.y,
               parentController.transform.position.z + zPos);
    }

}
