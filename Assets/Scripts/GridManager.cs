using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    GridSpawner spawner;
    int dim_x = 8;
    int dim_z = 8;
    TileGridArr TileArr;
    // Start is called before the first frame update
    void Start()
    {
        spawner = gameObject.GetComponent<GridSpawner>();
        dim_x = spawner.dim_x;
        dim_z = spawner.dim_z;

        TileArr = spawner.Tiles;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            // funDelete(); 
        }
    }


    // Returns the idx based on the player position and side he is on.
    // If his x/z pos is smaller 0 the idx for the array needs to be increased (i.e. 0, 1, ,2, 3)
    // Otherwise it is the other way around (8, 7, 6, 5 ...)
    private int[,] getArrIndex(int side, Vector3 player_position)
    {
        int arr_len;
        if(side % 2 == 0) 
        {
            arr_len = dim_z;
        } 
        else
        {
            arr_len = dim_x;
        } 

        int[,] idx = new int[arr_len, 2];

        int multi = side == 1 || side == 2 ? -1 : 1;

        int x = (int) player_position.x;
        int z = (int) player_position.z;

        if(side % 2 == 0)
        {
            for(int i = 0; i < arr_len; ++i)
            {
                idx[i, 0] = x;
                idx[i, 1] = z + (i+1) * multi;
            }
        }
        else
        {
            for(int i = 0; i < arr_len; ++i)
            {
                idx[i, 0] = x + (i+1) * multi;
                idx[i, 1] = z;
            }
        }

        return idx;
    }

    public int getPlayerSide(Vector3 player_position)
    {
        /* 
        0: S
        1: E
        2: N
        3: W
        */
        int side = -1; 

        int up = player_position.z <= 0.0f ? 0 : -1;
        int left = player_position.x <= 0.0f ? 0 : -1;

        up += player_position.z >= dim_z ? 2 : 0;
        left += player_position.x >= dim_x ? 2 : 0;

        if (up == 0)
        {
            side = 0;
        }
        else if(left == 0)
        {
            side = 3;
        }
        else if(left == 1)
        {
            side = 1;
        }
        else if(up == 1)
        {
            side = 2;
        }      
        else
        {
            side = -1;
        }  

        return side;
    }

    public TileSelector[] playerInteraction(Vector3 player_position)
    {   
        int side = getPlayerSide(player_position);
        int[,] idx = getArrIndex(side, player_position);

        TileSelector[] Tiles = new TileSelector[idx.GetLength(0)];

        for(int i = 0; i < idx.GetLength(0); ++i)
        {
            Tiles[i] = TileArr.getTile(idx[i,0], idx[i, 1]);
        }
        return Tiles;
    }

    // void funDelete()
    // {
    //     TileSelector[] Tiles = playerInteraction(new Vector3(5, 0, -1)); 

    //     for(int i = 0; i < Tiles.Length; ++i)
    //     {
    //         Destroy(Tiles[i].gameObject);
    //     }
    // }
}
