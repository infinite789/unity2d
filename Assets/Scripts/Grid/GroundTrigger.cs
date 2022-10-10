using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    public bool isEnabled;
    private CompositeCollider2D groundCollider;
    private CapsuleCollider2D playerCollider;
    private CompositeCollider2D[] elevationColliders;
    private ElevationTrigger[] elevationTriggers;
    private Transform player;

    private float offset = 2f;
    private float minOffset = 1.5f;
    private float maxOffset = 6f;


    // Start is called before the first frame update
    void Start()
    {
        // state
        isEnabled = false;

        // player transform
        player = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        player.position = new Vector3(player.position.x, player.position.y, offset);

        // elevation objects
        GameObject[] elevationObjects = GameObject.FindGameObjectsWithTag("elevation-collider");

        // colliders
        groundCollider = GameObject.FindGameObjectWithTag("ground-collider").GetComponent<CompositeCollider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("player").GetComponent<CapsuleCollider2D>();
        elevationColliders = new CompositeCollider2D[elevationObjects.Length];
        for (int i = 0; i < elevationObjects.Length; i++)
        { 
            elevationColliders[i] = elevationObjects[i].GetComponent<CompositeCollider2D>();
        }

        GameObject[] elevationTriggerObject = GameObject.FindGameObjectsWithTag("elevation-trigger");
        // triggers
        elevationTriggers = new ElevationTrigger[elevationObjects.Length];
        for (int i = 0; i < elevationTriggerObject.Length; i++)
        {
            elevationTriggers[i] = elevationTriggerObject[i].GetComponent<ElevationTrigger>();
        }

        // ignore collisions with elevations on start
        for (int i = 0; i < elevationObjects.Length; i++)
        {
            Physics2D.IgnoreCollision(playerCollider, elevationColliders[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (isEnabled == false)
        {
            // do your things here that has to happen once
            isEnabled = true;

            if (IsElevated())
            {
                offset = maxOffset;
            }
            else
            {
                offset = minOffset;
            }
        }



        player.position = new Vector3(player.position.x, player.position.y, offset);
        //Debug.Log("ground-trigger: " + isEnabled);
        //Debug.Log("position: " + player.position.z);

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        isEnabled = false;
        if(IsElevated())
        {
            offset = maxOffset;
        } 
        else
        {
            offset = minOffset;
        }
        player.position = new Vector3(player.position.x, player.position.y, offset);
        //Debug.Log("ground-trigger: " + isEnabled);
        //Debug.Log("position: " + player.position.z);
    }

    private bool IsElevated()
    {
        bool isElevated = false;
        for (int i = 0; i < elevationTriggers.Length; i++)
        {
            if (elevationTriggers[i].isEnabled)
            {
                isElevated = true;
                break;
            }
        }
        return isElevated;
    }
}
