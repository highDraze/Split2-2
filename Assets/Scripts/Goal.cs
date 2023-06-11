using UnityEngine;
using UnityEngine.InputSystem;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<Player>().goalInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<Player>().goalInRange = false;
    }
}