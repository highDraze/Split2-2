using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool isDM;
    public bool goalInRange;
    public bool goalGrabbed;

    public float invisibilityCooldown = 5f;

    private Vector3 invisibilityPosition;
    private bool invisible;
    private bool invisibilityActive = true;

    public int playerNumber;

    void Awake()
    {
        //GetComponent<Reachability>().ChangeReachability += ChangeReachability;
    }

    void Start()
    {
        FindObjectOfType<PlayerInputHandler>().PlayerJoined(this);
        GetComponent<Reachability>().target = FindObjectOfType<Goal>().transform;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (isDM) InstantiateTileAndMove();

        if (goalInRange) GrabGoal();
    }

    public void MakeInvisible(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (isDM) return;

        if (!invisibilityActive) AddInvisibility();
    }

    /*void ChangeReachability(bool reachable)
    {
        if (isDM) return;

        Debug.Log("INVOKE INVIS");

        if (!reachable) StartCoroutine(GiveInvisibilityCoroutine());
        else StartCoroutine(RemoveInvisibilityCoroutine());
    }*/

    /*
    IEnumerator GiveInvisibilityCoroutine()
    {
        yield return new WaitForSeconds(invisibilityDelay);

        if (!GetComponent<Reachability>().Reachable)
        {
            AddInvisibility();
        }
    }

    IEnumerator RemoveInvisibilityCoroutine()
    {
        yield return new WaitForSeconds(visibilityDelay);
        RemoveInvisibility();
    }
    */

    IEnumerator ReactivateInvisibility()
    {
        yield return new WaitForSeconds(invisibilityCooldown);
        invisibilityActive = true;
    }

    private void AddInvisibility()
    {
        invisibilityActive = false;
        int layer = LayerMask.NameToLayer("Invisible");
        gameObject.layer = layer;
        Color col = GetComponentInChildren<SpriteRenderer>().color;
        GetComponentInChildren<SpriteRenderer>().color = new Color(col.r, col.g, col.b, 0.6f); ;
        invisibilityPosition = transform.position;
        invisible = true;
    }

    private void RemoveInvisibility()
    {
        int layer = LayerMask.NameToLayer("Player");
        gameObject.layer = layer;
        Color col = GetComponentInChildren<SpriteRenderer>().color;
        GetComponentInChildren<SpriteRenderer>().color = new Color(col.r, col.g, col.b, 1f); ;
        invisibilityPosition = new Vector3(-100, -100, -100);
        invisible = false;
    }

    void GrabGoal()
    {
        goalGrabbed = true;
        GetComponent<Reachability>().target = FindObjectOfType<ExitGoal>().transform;
    }

    void Update()
    {
        if (invisible && (invisibilityPosition - transform.position).magnitude > 1)
        {
            RemoveInvisibility();
        }
    }

    void InstantiateTileAndMove()
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
                vector2Int.x = spawner.dim_x;
                break;
            case 2:
                vector2Int.y = spawner.dim_z;
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
        if (tiles == null)
        {
            Debug.Log("Cant move");
            Destroy(newTile.gameObject);
            return;
        }

        GridMover.Direction direction = (GridMover.Direction)manager.getPlayerSide(transform.position);
        FindObjectOfType<GridMover>().MoveTiles(tiles, direction);
    }
}