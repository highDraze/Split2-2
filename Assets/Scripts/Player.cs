using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool isDM;

    void Start()
    {
        FindObjectOfType<PlayerInputHandler>().PlayerJoined(this);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);

        if (isDM && Input.GetKeyDown(KeyCode.Space))
        {
            GridManager manager = FindObjectOfType<GridManager>();
            GridMover mover = FindObjectOfType<GridMover>();
            GridSpawner spawner = FindObjectOfType<GridSpawner>();

            int side = manager.getPlayerSide(transform.position);

            Vector2Int vector2Int = manager.playerToGridPosition(transform.position);
            switch (side)
            {
                case 0:
                    vector2Int.y = -1;
                    break;
                case 1:
                    vector2Int.x = spawner.dim_x + 1;
                    break;
                case 2:
                    vector2Int.y = spawner.dim_z + 1;
                    break;
                case 3:
                    vector2Int.x = -1;
                    break;
                default:
                    Debug.Log("Side not in [0,4]");
                    break;
            }


            TileSelector newTile = mover.InstantiateNewTile(vector2Int.x, vector2Int.y);

            TileSelector[] tiles = manager.moveGrid(transform.position, newTile);
            GridMover.Direction direction = (GridMover.Direction)manager.getPlayerSide(transform.position);
            FindObjectOfType<GridMover>().MoveTiles(tiles, direction);
        }
    }
}