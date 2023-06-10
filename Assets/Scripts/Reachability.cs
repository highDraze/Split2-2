using UnityEngine;
using UnityEngine.AI;

public class Reachability : MonoBehaviour
{
    public Transform target;

    private bool _Reachable;
    public bool Reachable
    {
        get
        {
            return _Reachable;
        }
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
            _Reachable = true;
        }
        else
        {
            _Reachable = false;
        }
    }
}