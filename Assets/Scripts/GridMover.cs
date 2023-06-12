using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class GridMover : MonoBehaviour
{
    public float MoveDuration = 0.6f;

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

    }

    public TileSelector InstantiateNewTile(int x, int z)
    {
        // Instantiate new Tile
        GameObject child = Instantiate(TileToSpawn, transform);

        child.transform.localPosition = new Vector3(x, -1.0f, z);
        TileSelector selector = child.GetComponent<TileSelector>();

        selector.tiletype = (TileSelector.TileTypes)Random.Range(1, 4);
        selector.rotation = (TileSelector.Rotation)Random.Range(0, 4);

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
            if(tile == tiles.First())
            {
                tile.transform.DOMove(tile.transform.position + new Vector3(0, 1, 0) + toOffset, MoveDuration);
            }
            else if (tile == tiles.Last())
            {   
                tile.transform.DOMove(tile.transform.position + new Vector3(0, -1.4f, 0) + toOffset, MoveDuration);
            }
            else
            {
                tile.transform.DOMove(tile.transform.position + toOffset, MoveDuration);
            }

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
        Destroy(GameObject.FindWithTag("MoveCollider"));
    }
}