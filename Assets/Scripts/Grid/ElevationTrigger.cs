using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevationTrigger : MonoBehaviour
{
    public bool isEnabled;
    private CapsuleCollider2D playerCollider;
    private CompositeCollider2D groundCollider;
    private CompositeCollider2D[] elevationColliders;
    private ElevationTrigger[] elevationTriggers;
    private GroundTrigger groundTrigger;
    private Transform player;
    private float offset = 2f;
    private float minOffset = 1.5f;
    private float maxOffset = 6f;

    private void Start()
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
        playerCollider = GameObject.FindGameObjectWithTag("character").GetComponent<CapsuleCollider2D>();
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
        groundTrigger = GameObject.FindGameObjectWithTag("ground-trigger").GetComponent<GroundTrigger>();

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
            if(groundTrigger.isEnabled)
            {
                offset = maxOffset;
                Physics2D.IgnoreCollision(playerCollider, groundCollider, isEnabled);
                for (int i = 0; i < elevationColliders.Length; i++)
                {
                    Physics2D.IgnoreCollision(playerCollider, elevationColliders[i], !isEnabled);
                }
            }
        }
 
        player.position = new Vector3(player.position.x, player.position.y, offset);
        //Debug.Log("elevation-trigger: " + isEnabled);
        //Debug.Log("position: " + player.position.z);

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        isEnabled = false;
        if(groundTrigger.isEnabled)
        {
            Physics2D.IgnoreCollision(playerCollider, groundCollider, isEnabled);
            for (int i = 0; i < elevationColliders.Length; i++)
            {
                Physics2D.IgnoreCollision(playerCollider, elevationColliders[i], !isEnabled);
            }
            offset = minOffset;
        }
      
        player.position = new Vector3(player.position.x, player.position.y, offset);
        //Debug.Log("elevation-trigger: " + isEnabled);
        //Debug.Log("position: " + player.position.z);

    }

}
