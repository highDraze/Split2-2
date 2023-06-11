using UnityEngine;
using UnityEngine.AI;

public class Reachability : MonoBehaviour
{
    // public Transform target;

    // public delegate void DChangeReachability(bool reachability);
    // public event DChangeReachability ChangeReachability;

    // public bool Reachable
    // {
    //     get; private set;
    // }

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<NavMeshAgent>().destination = target.position;
        // ChangeReachability?.Invoke(Reachable);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // NavMeshPath path = new NavMeshPath();
        // NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

        //Debug.Log($"Reachble {Reachable}");

        // if (path.status == NavMeshPathStatus.PathComplete)
        // {
        //     if (!Reachable)
        //     {
        //         Reachable = true;
        //         ChangeReachability?.Invoke(true);
        //     }
        // }
        // else
        // {
        //     if (Reachable)
        //     {
        //         Reachable = false;
        //         ChangeReachability?.Invoke(false);
        //     }
        // }
    }
}