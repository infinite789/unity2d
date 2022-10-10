using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleController : MonoBehaviour
{
    public Renderer renderer;
    public TilemapRenderer tilemap; 

    // Start is called before the first frame update
    void Start()
    {
        renderer.sortingOrder = (int)(transform.position.y * -10);
        Debug.Log("sortingOrder: " + renderer.sortingOrder);

        //BoundsInt bounds = tilemap.cellBounds;
        //TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        //for (int x = 0; x < bounds.size.x; x++)
        //{
        //    for (int y = 0; y < bounds.size.y; y++)
        //    {
        //        TileBase tile = allTiles[x + y * bounds.size.x];
        //        if(tile != null)
        //        {
                    
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
