using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Transform DMSpawnPosition;
    public List<Transform> PlayerSpawnPositions;

    private PlayerInput playerInput;
    private PlayerInputManager manager;

    private List<Player> players = new List<Player>();

    public void PlayerJoined(Player player)
    {
        players.Add(player);
        player.playerNumber = players.Count - 1;

        if (players.Count == 1)
        {
            player.isDM = true;
            player.gameObject.layer = 9;
            player.transform.position = DMSpawnPosition.position;
        }
        else
        {
            player.gameObject.layer = 6;
            player.transform.position = PlayerSpawnPositions[players.Count - 2 % 4].position;
            FindObjectOfType<Timer>().stopTimer = false;
        }
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

}