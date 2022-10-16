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
    public string currentWeapon = "sword";
    public List<string> weapons = new List<string> { "sword", "hammer" };

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Awake Weapon");
        stateController = GameObject.FindObjectOfType<StateController>();

        gameObject.SetActive(stateController.hasWeapon);

    }
    private void OnEnable()
    {
        for (int i = 0; i < weaponLayerControllers.Length; i++)
        {
            Debug.Log("syncing");
            weaponLayerControllers[i].anim.animationCounter = 0;
            weaponLayerControllers[i].anim.spriteId = 0;
        }

        for (int i = 0; i < characterLayerControllers.Length; i++)
        {
            Debug.Log("syncing");
            characterLayerControllers[i].anim.animationCounter = 0;
            characterLayerControllers[i].anim.spriteId = 0;
        }

        if (stateController.getCurrentOrientationString().Contains("up"))
        {
            SetZPos(-0.5f);
        }
        else
        {
            SetZPos(0.0f);
        }
    }

    private void Update()
    {
        if (stateController.isFiring && stateController.hitTimer <= 0)
        {
            stateController.isFiring = false;
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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

                for (int i = 0; i < weaponLayerControllers.Length; i++)
                {
                    Debug.Log("syncing");
                    weaponLayerControllers[i].anim.animationCounter = 0;
                    weaponLayerControllers[i].anim.spriteId = 0;
                }

                for (int i = 0; i < characterLayerControllers.Length; i++)
                {
                    Debug.Log("syncing");
                    characterLayerControllers[i].anim.animationCounter = 0;
                    characterLayerControllers[i].anim.spriteId = 0;
                }
            }
            if (stateController.getCurrentOrientationString().Contains("up"))
            {
                SetZPos(-0.5f);
            }
            else
            {
                SetZPos(0f);
            }
        }
    }

    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();

        if (stateController.getCurrentOrientationString().Contains("up"))
        {
            SetZPos(-0.5f);
        }
        else
        {
            SetZPos(0f);
        }
    }

    public void OnSwitchWeapon()
    {
        int id = weapons.FindIndex(weapon => weapon.Contains(currentWeapon));
        id = (id + 1) % weapons.Count;
        currentWeapon = weapons[id];

        for (int i = 0; i < weaponLayerControllers.Length; i++)
        {
            weaponLayerControllers[i].currentWeapon = currentWeapon;
        }


        for (int i = 0; i < weaponLayerControllers.Length; i++)
        {
            Debug.Log("syncing");
            weaponLayerControllers[i].anim.animationCounter = 0;
            weaponLayerControllers[i].anim.spriteId = 0;
        }

        for (int i = 0; i < characterLayerControllers.Length; i++)
        {
            Debug.Log("syncing");
            characterLayerControllers[i].anim.animationCounter = 0;
            characterLayerControllers[i].anim.spriteId = 0;
            characterLayerControllers[i].anim.appendix = currentWeapon;
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
