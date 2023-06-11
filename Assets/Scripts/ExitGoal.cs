using UnityEngine;
using UnityEngine.InputSystem;

public class ExitGoal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<Player>().goalGrabbed)
        {
            FindObjectOfType<Button_Menu>().EndScenePlayerWon();
        }
    }
}