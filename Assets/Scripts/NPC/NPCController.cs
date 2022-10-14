using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public bool isEnabled = false;
    private GameObject glow;
    private GameObject menu;
    private GameObject player;
    private GameObject weapon;
    private StateController stateController;
    private WeaponController weaponController;
    public CharacterLayerController[] layerControllers;

    private void Start()
    {
        glow = FindGameObject.InChildWithTag(gameObject, "glow");
        menu = FindGameObject.InChildWithTag(gameObject, "menu");
        player = GameObject.FindGameObjectWithTag("player");
        weaponController = FindGameObject.InChildWithTag(player, "weapon").GetComponent<WeaponController>();
        stateController = player.GetComponentInChildren<StateController>();
        glow.SetActive(false);
        menu.SetActive(false);
        this.set();

    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isEnabled)
        {
            isEnabled = true;
        }
        this.set();

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnabled = false;
        this.set();

    }
    
    public void OnInteract()
    {
        Debug.Log(weaponController.ToString());

        if(isEnabled && !stateController.hasSword)
        {
            Debug.Log("before syncing");

            stateController.hasSword = true;
            weaponController.gameObject.SetActive(true);
            for (int i = 0; i < layerControllers.Length; i++)
            {
                Debug.Log("syncing");

                layerControllers[i].anim.animationCounter = 0;
                layerControllers[i].anim.spriteId = 0;

            }

            weaponController.anim.animationCounter = 0;
            weaponController.anim.spriteId = 0;
            string animationString = stateController.getCurrentClass() + "_" + stateController.getCurrentOrientationString();
            //Debug.Log(animationString);
            if (animationString.Contains("up"))
            {
                //Debug.Log("contains_Up");
                weaponController.SetZPos(-1f);

            }
            else 
            {
                //Debug.Log("does not contain up");
                weaponController.SetZPos(0.5f);
            }

            weaponController.flipXOnMove();
        }
    }

    private void set()
    {
        if (isEnabled)
        {
            glow.SetActive(true);
            menu.SetActive(true);
        }
        else
        {
            glow.SetActive(false);
            menu.SetActive(false);
        }
    }
  
}

public static class FindGameObject
{
    public static GameObject InChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }
} 
