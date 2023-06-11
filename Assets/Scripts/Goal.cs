using UnityEngine;
using UnityEngine.InputSystem;

public class Goal : MonoBehaviour
{
    public GameObject treasure;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<Player>().goalGrabbed == false)
        {
            other.gameObject.GetComponent<Player>().goalGrabbed = true;

            FindObjectOfType<GridManager>().randomize_field();

            treasure.SetActive(false);
        }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.tag == "Player")
    //         other.gameObject.GetComponent<Player>().goalInRange = false;
    // }
}