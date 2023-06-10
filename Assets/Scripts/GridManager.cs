using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
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

        side = 2 * up + left; 
        
    }
}
