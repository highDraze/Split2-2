using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileGridArr 
{
    [SerializeField]
    private TileSelector[] Tiles;
    [SerializeField]
    private int dim_x, dim_z;

   public TileGridArr(int _dim_x, int _dim_z)
   {
        dim_x = _dim_x;
        dim_z = _dim_z;
        Tiles = new TileSelector[dim_z * dim_x];
   }

    public void setTile(int x, int z, TileSelector tile)
    {
        Tiles[z * dim_x + x] = tile;
    }

    public TileSelector getTile(int x, int z)
    {
        return Tiles[z * dim_x + x];
    }
  

}
