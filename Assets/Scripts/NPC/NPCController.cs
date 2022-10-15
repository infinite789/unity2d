using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public bool isEnabled = false;
    private GameObject glow;
    private GameObject menu;
    private GameObject player;
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

        if(isEnabled && !stateController.hasWeapon)
        {
            stateController.hasWeapon = true;
            weaponController.gameObject.SetActive(true);
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
