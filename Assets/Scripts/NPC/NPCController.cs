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
    private CharacterLayerController swordController;
    private CharacterLayerController[] layerControllers;

    private void Start()
    {
        glow = FindGameObject.InChildWithTag(gameObject, "glow");
        menu = FindGameObject.InChildWithTag(gameObject, "menu");
        player = GameObject.FindGameObjectWithTag("player");
        weapon = FindGameObject.InChildWithTag(player, "weapon");
        swordController = FindGameObject.InChildWithTag(weapon, "sword").GetComponent<CharacterLayerController>();
        layerControllers = player.GetComponentsInChildren<CharacterLayerController>();
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

        if(isEnabled && !swordController.hasSword)
        {
            swordController.hasSword = true;
            swordController.gameObject.SetActive(true);
            for (int i = 0; i < layerControllers.Length; i++)
            {
                layerControllers[i].hasSword = true;
                layerControllers[i].anim.animationCounter = 0;
                layerControllers[i].anim.spriteId = 0;
            }

            string animationString = swordController.anim.currentAnimation;

            if (animationString.Contains("up"))
            {
                swordController.SetZPos(-1f);

            }
            else 
            {
                swordController.SetZPos(0.5f);
            }

            swordController.flipXOnMove();
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
