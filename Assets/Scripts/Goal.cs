using UnityEngine;
using UnityEngine.InputSystem;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Player")
            interactable = true;*/
    }

    void OnTriggerExit(Collider other)
    {
        /*if (other.tag == "Player")
            interactable = false;*/
    }
}