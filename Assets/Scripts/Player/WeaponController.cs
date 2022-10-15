using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    private WeaponLayerController activeWeaponLayer;
    private Vector2 moveInput;
    public CharacterController characterController;
    public CharacterLayerController[] characterLayerControllers;
    public WeaponLayerController[] weaponLayerControllers;
    private StateController stateController;
    public string animationString;
    public string defaultWeapon = "sword";

    private bool isActive = false;
    public bool _isActive
    {
        get { return isActive; }
        set
        {
            if (isActive == value)
                return;

            isActive = value;

            if (OnToggleActive != null)
                OnToggleActive(isActive);
        }
    }
    public delegate void ToggleActive(bool newVal);
    public static event ToggleActive OnToggleActive;


    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Awake Weapon");

        OnToggleActive += ToggleLayerHandler;

        stateController = GameObject.FindObjectOfType<StateController>();

        gameObject.SetActive(stateController.hasWeapon);
    }
    private void ToggleLayerHandler(bool newVal)
    {
        //do whatever
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
        string currentOrientation = stateController.getCurrentOrientationString();
        string currentClass = stateController.getCurrentClass();

        animationString = currentClass + "_" + currentOrientation;

        //Debug.Log(hitTimer);
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

        if (animationString.Contains("up"))
        {
            SetZPos(-0.6f);
        }
        else
        {
            SetZPos(0.15f);
        }
    }

    public void SetZPos(float zPos)
    {
        this.transform.position = new Vector3(
               characterController.transform.position.x,
               characterController.transform.position.y,
               characterController.transform.position.z + zPos);
    }
}
