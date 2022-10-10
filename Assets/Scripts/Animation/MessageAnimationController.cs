using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAnimationController : MonoBehaviour
{
    public float animationSpeed;
    public float maxSize;
    public float minSize;
    private float dx;
    private bool isGrowing = true;
    private int animationCounter = 0;
    private Transform transform;
    public int skipFrames;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        dx = minSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(animationCounter % skipFrames == 0)
        {

            transform.localScale = new Vector3(maxSize * dx, maxSize * dx, 1);

            if (isGrowing)
            {
                dx += animationSpeed;
            }
            else
            {
                dx -= animationSpeed;
            }

            if (dx > maxSize || dx < minSize)
            {
                isGrowing = !isGrowing;
            }
        }

        animationCounter++;
    }
}
