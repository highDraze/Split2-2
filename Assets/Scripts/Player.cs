using UnityEngine;
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
            TileSelector[] tiles = manager.playerInteraction(transform.position);
            GridMover.Direction direction = (GridMover.Direction)manager.getPlayerSide(transform.position);
            FindObjectOfType<GridMover>().MoveTiles(tiles, direction);
        }
    }
}