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

    TileSelector [,] tiles;
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

        tiles = new TileSelector[dim_z, dim_x];
        for(int x = 0; x < dim_x; ++x)
        {
            for(int z = 0; z < dim_z; ++z)
            {
                GameObject temp = (GameObject) PrefabUtility.InstantiatePrefab(tile_selector);
                temp.transform.parent = transform;
                temp.transform.position = new Vector3(x, 0, z);
                tiles[z, x] = temp.GetComponent<TileSelector>();

                if(create_rand)
                {
                    tiles[z, x].tiletype = (TileSelector.TileTypes)Random.Range(1, 5);
                    tiles[z, x].ChangeTileType();
                }
            }
        }
    }
    # endif
}
