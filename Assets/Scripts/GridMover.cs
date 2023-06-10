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


    public void MoveTiles(TileSelector[] tiles, Direction direction)
    {
        switch(direction)
        {
            case Direction.UP:
                transform.DOMove(transform.position + new Vector3(0, 0, 1), MoveDuration);
                StartCoroutine(MoveCoroutine());
                break;
            case Direction.RIGHT:
                break;
            case Direction.DOWN:
                break;
            case Direction.LEFT:
                break;
        }
    }

    IEnumerator MoveCoroutine()
    {
        StartMove?.Invoke();
        yield return new WaitForSeconds(MoveDuration);
        EndMove?.Invoke();
    }
}