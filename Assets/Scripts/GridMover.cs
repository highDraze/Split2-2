using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class GridMover : MonoBehaviour
{
    public float MoveDuration = 1f;

    public delegate void DStartMove();
    public static event DStartMove StartMove;
    public delegate void DEndMove();
    public static event DStartMove EndMove;

    public GameObject TileToSpawn;


    public enum Direction
    {
        UP,
        LEFT,
        DOWN,
        RIGHT
    }

    void Start()
    {
        /*TileGridArr tileGridArr = FindObjectOfType<GridSpawner>().Tiles;

        for (int i=0; i<8; i += 1)
        {
            tiles.Add(tileGridArr.getTile(i, 5));
        }


        MoveTiles(tiles.ToArray(), Direction.DOWN);*/

    }

    public TileSelector InstantiateNewTile(int x, int z)
    {
        // Instantiate new Tile
        GameObject child = Instantiate(TileToSpawn, transform);
        //child.transform.Rotate(0, 90 * (int)rotation, 0, Space.Self);
        child.transform.localPosition = new Vector3(x, 0, z);
        TileSelector selector = child.GetComponent<TileSelector>();

        selector.tiletype = (TileSelector.TileTypes)Random.Range(1, 5);
        //var rotation = Random.Range(0, 4);
        //selector.transform.Rotate(Vector3.up, 90f * rotation);

        return selector;
    }


    public void MoveTiles(IEnumerable<TileSelector> tiles, Direction direction)
    {
        if (tiles == null) return;

        Vector3 toOffset = Vector3.zero;
        switch(direction)
        {
            case Direction.UP:
                toOffset = new Vector3(0, 0, 1);
                break;
            case Direction.RIGHT:
                toOffset = new Vector3(1, 0, 0);
                break;
            case Direction.DOWN:
                toOffset = new Vector3(0, 0, -1);
                break;
            case Direction.LEFT:
                toOffset = new Vector3(-1, 0, 0);
                break;
        }

        Debug.Log($"move direction ${direction}");

        foreach(TileSelector tile in tiles)
        {
            tile.transform.DOMove(tile.transform.position + toOffset, MoveDuration);
        }
        StartCoroutine(MoveCoroutine(tiles));
    }

    IEnumerator MoveCoroutine(IEnumerable<TileSelector> tiles)
    {
        var dim_x = FindObjectOfType<GridSpawner>().dim_x;
        var dim_z = FindObjectOfType<GridSpawner>().dim_z;

        StartMove?.Invoke();
        yield return new WaitForSeconds(MoveDuration);
        EndMove?.Invoke();

        foreach (TileSelector tile in tiles)
        {
            tile.isMovable = true;
            /*if (tile.transform.position.x >= dim_x || tile.transform.position.z >= dim_z || tile.transform.position.x < 0 || tile.transform.position.z < 0)
            {
                Destroy(tile.transform.gameObject);
            }*/
        }
        Destroy(tiles.Last().transform.gameObject);
    }
}