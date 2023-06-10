using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GridMover : MonoBehaviour
{
    public float MoveDuration = 1f;

    public delegate void DStartMove();
    public static event DStartMove StartMove;
    public delegate void DEndMove();
    public static event DStartMove EndMove;

    public enum Direction
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    void Start()
    {
        // TODO remove that

        FindObjectOfType<GridSpawner>();
    }


    public void MoveTiles(TileSelector[] tiles, Direction direction)
    {
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

        transform.DOMove(transform.position + toOffset, MoveDuration);
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        StartMove?.Invoke();
        yield return new WaitForSeconds(MoveDuration);
        EndMove?.Invoke();
    }
}