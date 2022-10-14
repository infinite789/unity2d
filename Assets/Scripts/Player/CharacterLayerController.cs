using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLayerController : MonoBehaviour
{
    public CharacterController parentController;
    public AnimationController anim = new();
    private Vector2 moveInput;
    public GameObject player;
    public CharacterLayerController[] layerControllers;
    private StateController stateController;
    private string animationString;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
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
        animationString = stateController.getCurrentClass() + "_" + stateController.getCurrentOrientationString();
        anim.UpdateAnimation(moveInput);
       
        //Debug.Log(hitTimer);

       

     
    }

       
    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        flipXOnMove();

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

}
