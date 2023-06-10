using UnityEngine;
using UnityEngine.AI;

public class Reachability : MonoBehaviour
{
    public Transform target;

    public bool Reachable
    {
        get; private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<NavMeshAgent>().destination = target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            Reachable = true;
        }
        else
        {
            Reachable = false;
        }
    }
}