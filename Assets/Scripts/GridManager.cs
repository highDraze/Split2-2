using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    GridSpawner spawner;
    int dim_x = 8;
    int dim_z = 8;
    public TileGridArr TileArr;
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

    }

    public bool playerIsOnGridSide(Vector3 player_pos)
    {
        int in_x = 0;
        int in_z = 0;
        if(player_pos.x < dim_x - 0.5 && player_pos.x > -0.5)
        {
            in_x = 1;
        } 
        if(player_pos.z < dim_z - 0.5 && player_pos.z > -0.5)
        {
            in_z = 1;
        }

        Debug.Log($"player on grid side {in_x}, {in_z}");

        return in_x + in_z == 1;
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

        /*for(int i = 0; i < arr_len; ++i)
        {
            Debug.Log($"0 : {idx[i, 0]}, 1 : {idx[i, 1]}");
        }*/

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

        int up = player_position.z <= -0.5f ? 0 : -1;
        int left = player_position.x <= -0.5f ? 0 : -1;

        up += player_position.z >= dim_z - 0.5f ? 2 : 0;
        left += player_position.x >= dim_x - 0.5f ? 2 : 0;

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

    public Vector2Int playerToGridPosition(Vector3 player_pos, bool snap = true)
    {
        int x = Mathf.RoundToInt(player_pos.x);
        int z = Mathf.RoundToInt(player_pos.z);
        int side = getPlayerSide(player_pos);

        if(snap)
        {
            switch(side)
        {
            case 0:
                z = 0;
                break;
            case 1:
                x = dim_x - 1;
                break;
            case 2:
                z = dim_z - 1;
                break;
            case 3:
                x = 0;
                break;
            default:
                Debug.Log("Side not in [0,4]");
                break;
        }
        }
        return new Vector2Int(x, z);
    }

    // Returns Tiles in the interaction line
    // otherwise returns null (need to check!!)
    public TileSelector[] moveGrid(Vector3 player_position, TileSelector newTile)
    {   
        if(!playerIsOnGridSide(player_position))
        {
            return null;
        }
        int side = getPlayerSide(player_position);
        int[,] idx = getArrIndex(side, player_position);
        int direction = side == 1 || side == 2 ? -1 : 1;


        TileSelector[] Tiles = new TileSelector[idx.GetLength(0) + 1];
        Tiles[0] = newTile;

        for(int i = 1; i < idx.GetLength(0) + 1; ++i)
        {
            TileSelector temp = TileArr.getTile(idx[i - 1,0], idx[i - 1, 1]);
            if(temp.isMovable)
            {
                Tiles[i] = temp;
            }
            else{
                return null;
            }
        }


        for(int i = 0; i < idx.GetLength(0); ++i)
        {
            //Debug.Log($"Setting {idx[i, 0]},{idx[i, 1]}");
            Tiles[i].isMovable = false;
            TileArr.setTile(idx[i,0], idx[i, 1], Tiles[i]);
        }
        Tiles[idx.GetLength(0)].isMovable = false;
        
        return Tiles;
    }

    public void randomize_field()
    {
        for(int x = 1 ; x < dim_x - 1; ++x)
        {
            for(int z = 1 ; z < dim_z - 1; ++z)
            {
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

                TileSelector tempTile = TileArr.getTile(x, z);
                tempTile.gameObject.GetComponentInChildren<TileProperties>().myType = (TileSelector.TileTypes)ranNum;
                tempTile.rotation = (TileSelector.Rotation)Random.Range(0, 4);
                tempTile.ChangeTileType();
            }   
        }
    }

}


