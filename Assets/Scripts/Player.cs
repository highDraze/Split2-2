using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<PlayerInputHandler>().PlayerJoined(this);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
    }
}