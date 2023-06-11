using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
    using UnityEditor;
# endif

public class BoundaryGridManager : MonoBehaviour
{
    public int dim_x = 8;
    public int dim_z = 8;
    public GameObject Boundary;

    // Start is called before the first frame update
    void Start()
    {

    }

 [ContextMenu("Create Grid")]
    void CreateGrid()
    {
   # if UNITY_EDITOR

        if(transform.childCount > 0)
        {
            for(int i = transform.childCount - 1; i >= 0; --i)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        for(int x = 0; x < 2; ++x)
        {
            for(int z = 0; z < dim_z; ++z)
            {
                GameObject temp = (GameObject) PrefabUtility.InstantiatePrefab(Boundary);
                temp.transform.parent = transform;
                temp.transform.position = new Vector3(0.0f + x * (dim_x - 1), 0, z);
                temp.transform.Rotate(0.0f, 90.0f + x * 180.0f, 0.0f, Space.Self);
            }
        }

        for(int x = 0; x < dim_x; ++x)
        {
            for(int z = 0; z < 2; ++z)
            {
                GameObject temp = (GameObject) PrefabUtility.InstantiatePrefab(Boundary);
                temp.transform.parent = transform;
                temp.transform.position = new Vector3(x, 0, 0.0f + z * (dim_z - 1));
                temp.transform.Rotate(0.0f, 0.0f + z * 180.0f, 0.0f, Space.Self);
            }
        }
        # endif
    }
}
