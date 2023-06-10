using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    GridSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = gameObject.GetComponent<GridSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInteraction(transform.position);
    }

    void playerInteraction(Vector3 position)
    {   
        /* 
        0: S
        1: E
        2: N
        3: W
        */
        int side; 

        int up = position.z <= 0.0f ? 0 : -1;
        int left = position.x <= 0.0f ? 0 : -1;

        up += position.z >= spawner.dim_z ? 2 : 0;
        left += position.x >= spawner.dim_x ? 2 : 0;

        Debug.Log($"up : {up}, left : {left}");
        
    }
}
