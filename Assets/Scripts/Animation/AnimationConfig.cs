using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationConfig
{
    public string animationClass;
    public SubClassConfig[] subClasses;
    public bool isLeft = true;

    public int numberOfLayers()
    {
        int count = 0;
        foreach (SubClassConfig subClassConfig in this.subClasses)
        {
            count += subClassConfig.numberOfLayers;
        }
        return count;
    }
}

[System.Serializable]
public class SubClassConfig
{
    public Orientation subClass;
    public int numberOfLayers = 1;

}

public enum Orientation // your custom enumeration
{
    left = 1,
    right = 2,
    up = 3,
    down = 4,
};
