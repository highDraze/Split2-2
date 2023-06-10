﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class GridMover : MonoBehaviour
{
    public float MoveDuration = 1f;

    public delegate void DStartMove();
    public static event DStartMove StartMove;
    public delegate void DEndMove();
    public static event DStartMove EndMove;
    private List<TileSelector> tiles = new List<TileSelector>();


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


    public void MoveTiles(TileSelector[] tiles, Direction direction)
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
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        StartMove?.Invoke();
        yield return new WaitForSeconds(MoveDuration);
        EndMove?.Invoke();
    }
}