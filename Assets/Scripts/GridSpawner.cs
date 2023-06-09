using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
    using UnityEditor;
# endif

public class GridSpawner : MonoBehaviour
{
    public int dim_x = 8;
    public int dim_z = 8;
    public GameObject tile_selector;
    public bool create_rand = true;

    public TileGridArr Tiles;


    // Start is called before the first frame update
    void Start()
    {
        
    }

 [ContextMenu("Create Grid")]
    void CreateGrid()
    {
   # if UNITY_EDITOR

        if(transform.childCount > 0)
        {
            for(int i = transform.childCount - 1; i >= 0; --i)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        Tiles = new TileGridArr(dim_x, dim_z);
        for(int x = 0; x < dim_x; ++x)
        {
            for(int z = 0; z < dim_z; ++z)
            {
                GameObject temp = (GameObject) PrefabUtility.InstantiatePrefab(tile_selector);
                temp.transform.parent = transform;
                temp.transform.position = new Vector3(x, 0, z);
                Tiles.setTile(x, z, temp.GetComponent<TileSelector>());

                if(create_rand)
                {
                    TileSelector curTile = Tiles.getTile(x, z);
                    int ranNum = Random.Range(0, 4 * 4 + 2);
                    ranNum += 1;
                    if(ranNum <= 4)
                    {
                        ranNum = 1;
                    }
                    else if(ranNum <= 8)
                    {
                        ranNum = 2;
                    }
                    else if(ranNum <= 12)
                    {
                        ranNum = 3;
                    }
                    else if(ranNum <= 16)
                    {
                        ranNum = 4;
                    }
                    else if(ranNum <= 17)
                    {
                        ranNum = 5; 
                    }
                    else
                    {
                        ranNum = 6; 
                    }
                    
                    curTile.tiletype = (TileSelector.TileTypes) ranNum;
                    curTile.rotation = (TileSelector.Rotation)Random.Range(0,4);
                    curTile.ChangeTileType();
                }
            }
        }
        # endif
    }
}
