using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[System.Serializable]
public class AnimationController
{
    public AnimationConfig[] config;

    private float animationSpeed;

    public SpriteRenderer renderer;

    private Dictionary<string, Sprite[]> animations = new Dictionary<string, Sprite[]>();

    private Dictionary<string, int> framesPerSubclass = new Dictionary<string, int>();

    public int spriteId = 0;

    public float animationCounter = 0f;

    private StateController stateController;

    public Vector2 direction;

    public int layerId;

    public string appendix = "";

    public float relativeAnimationSpeed = 1;

    private CharacterLayerController playerController;

    public string currentAnimation;

    public void CreateAnimations(GameObject gameObject)
    {
        stateController = GameObject.FindObjectOfType<StateController>();

        playerController = gameObject.GetComponent<CharacterLayerController>();
        animationSpeed = stateController.animationSpeed;
        foreach (AnimationConfig animConfig in config)
        {
            var sprites = Resources.LoadAll<Sprite>("Sprites/Player/" + animConfig.animationClass);
            var frames = sprites.Length / animConfig.numberOfLayers();
            var startpoint = (layerId - 1) * frames;

            for (int i = 0; i < animConfig.subClasses.Length; i++)
            {

                int numberOfLayers = animConfig.subClasses[i].numberOfLayers;
                for (int j = 0; j < numberOfLayers; j++)
                {
                    if (j == (layerId - 1))
                    {
                        int endpoint = startpoint + frames;
                        string key = animConfig.animationClass + "_" + animConfig.subClasses[i].subClass.ToString();
                        Debug.Log("Layer ID: " + layerId);
                        Debug.Log("key: " + key);
                        Debug.Log(startpoint + "   :   " + endpoint);
                        Debug.Log("number of layers: " + numberOfLayers);
                        this.animations.Add(key, sprites[startpoint..endpoint]);
                        startpoint = startpoint + frames * numberOfLayers;

                    } 
                }
            }
            //Debug.Log(animConfig.animationClass);
            framesPerSubclass[animConfig.animationClass] = frames;

        }
    }

    public void UpdateAnimation(Vector2 direction)
    {
        if(currentAnimation != null)
        {

            this.currentAnimation = stateController.getCurrentClass();
            this.direction = direction;
            spriteId = (int)animationCounter % framesPerSubclass[currentAnimation];
            //Debug.Log("spriteId: " + spriteId);

            if (animations.ContainsKey(stateController.getCurrentAnimationString()))
            {

                renderer.sprite = getCurrentAnimation()[spriteId];
            }
            else
            {
                renderer.sprite = null;
            }
            animationCounter = (animationCounter + animationSpeed * Time.fixedDeltaTime * relativeAnimationSpeed);
            //Debug.Log( "animationCntr: " + animationCounter);

            if (stateController.getCurrentClass() == "idle_sword")
            {
                //Debug.Log(getCurrentAnimationString());
            }
        }
    }

   
    private Sprite[] getCurrentAnimation()
    {
        return animations[stateController.getCurrentAnimationString()];
    }


}
