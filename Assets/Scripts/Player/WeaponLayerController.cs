using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponLayerController : MonoBehaviour
{
    public AnimationController anim = new();
    private Vector2 moveInput;
    public GameObject player;
    private WeaponController parentController;
    private CharacterLayerController playerController;
    private CharacterController characterController;
    public CharacterLayerController[] characterLayerControllers;
    private StateController stateController;
    public string animationString;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Awake Weapon Layer");
       
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<CharacterLayerController>();
        parentController = this.transform.parent.GetComponent<WeaponController>();
        characterController = FindGameObject.InChildWithTag(player, "character").GetComponent<CharacterController>();
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

        if(this.tag == "weapon-" + parentController.defaultWeapon)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (stateController.isFiring && stateController.hitTimer <= 0)
        {
            stateController.isFiring = false;
            Debug.Log("hello my friend, i'm firing!!");
            string animationString = stateController.getCurrentClass() + "_" + stateController.getCurrentOrientationString();
            if (!animationString.Contains("up"))
            {
                SetZPos(-0f);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animationString = stateController.getCurrentClass() + "_" + stateController.getCurrentOrientationString();
        anim.UpdateAnimation(moveInput);
    }


    private void OnFire(InputValue input)
    {
        if (stateController.hasWeapon)
        {
            if (!stateController.isFiring && moveInput == Vector2.zero)
            {
                stateController.hitTimer = stateController.hitDuration;
                stateController.isFiring = true;
                Debug.Log("before syncing");

                for (int i = 0; i < characterLayerControllers.Length; i++)
                {
                    Debug.Log("syncing");
                    characterLayerControllers[i].anim.animationCounter = 0;
                    characterLayerControllers[i].anim.spriteId = 0;
                }
                anim.animationCounter = 0;
                anim.spriteId = 0;
            }
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
               characterController.transform.position.x,
               characterController.transform.position.y,
               characterController.transform.position.z + zPos);
    }

}
