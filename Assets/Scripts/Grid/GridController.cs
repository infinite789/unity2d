using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public float count;

    private void Start()
    {
        count = 0;
    }

    public void increment()
    {
        count++;
    }
}
