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
        // if(Input.GetKeyDown(KeyCode.K))
        // {
        //    funDelete(); 
        // }
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

        Vector2Int grid_pos = playerToGridPosition(player_position);
        int x = grid_pos.x;
        int z = grid_pos.y;

        if(side % 2 == 0)
        {
            for(int i = 0; i < arr_len; ++i)
            {
                idx[i, 0] = x;
                idx[i, 1] = z + i * multi;
            }
        }
        else
        {
            for(int i = 0; i < arr_len; ++i)
            {
                idx[i, 0] = x + i * multi;
                idx[i, 1] = z;
            }
        }

        return idx;
    }
    /* 
       0: S
       1: E
       2: N
       3: W
       */
    public int getPlayerSide(Vector3 player_position)
    {
       
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

    public Vector2Int playerToGridPosition(Vector3 player_pos)
    {
        int x = (int) player_pos.x;
        int z = (int) player_pos.z;
        int side = getPlayerSide(player_pos);

        switch(side)
        {
            case 0:
                z = 0;
                break;
            case 1:
                x = dim_x;
                break;
            case 2:
                z = dim_z;
                break;
            case 3:
                x = 0;
                break;
            default:
                Debug.Log("Side not in [0,4]");
                break;
        }
        return new Vector2Int(x, z);
    }

    // Returns Tiles in the interaction line
    // otherwise returns null (need to check!!)
    public TileSelector[] moveGrid(Vector3 player_position, TileSelector newTile)
    {   
        int side = getPlayerSide(player_position);
        int[,] idx = getArrIndex(side, player_position);
        int direction = side == 1 || side == 2 ? -1 : 1;


        TileSelector[] Tiles = new TileSelector[idx.GetLength(0)];

        for(int i = 0; i < idx.GetLength(0); ++i)
        {
            TileSelector temp = TileArr.getTile(idx[i,0], idx[i, 1]);
            if(temp.isMovable)
            {
                temp.isMovable = false;
                Tiles[i] = temp;
            }
            else{
                break;
            }
        }

        for(int i = 0; i < idx.GetLength(0); ++i)
        {

        }

        Tiles = null;
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


