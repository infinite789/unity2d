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
    private string _currentWeapon = "sword";
    public string currentWeapon
    {
        get 
        { 
            return _currentWeapon; 
        }
        set
        {
            _currentWeapon = value;
            OnChangeWeapon(value);
        }
    }

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

        if (this.tag == "weapon-" + parentController.currentWeapon)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
    private void ToggleLayerHandler(bool newVal)
    {
        Debug.Log("ToggleLayerHandler");
        this.transform.parent.gameObject.SetActive(stateController.hasWeapon);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        anim.UpdateAnimation(moveInput);
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

    public void OnChangeWeapon(string weapon)
    {
        Debug.Log(this.tag);
        Debug.Log(weapon);
        if(this.tag.Contains(weapon))
        {
            gameObject.SetActive(true);
        } else
        {
            gameObject.SetActive(false);
        }
        flipXOnMove();

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
