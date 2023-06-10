using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileSelector : MonoBehaviour
{
    public enum TileTypes { 
        Empty,
        Line ,
        Curve,
        T,
        Cross
        }

    public TileTypes tiletype;
    
    public GameObject[] Tiles = new GameObject[5]; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Change")]
    void test()
    { 
        Debug.Log("Blub");

        foreach(GameObject curObj in Tiles)
        {
            if (curObj.GetComponent<TileProperties>().myType == tiletype)
            {
                GameObject child = (GameObject)PrefabUtility.InstantiatePrefab(curObj);
                if(child == null)
                {
                    Debug.Log($"Error instantiating a tile of type {tiletype}");
                }
                child.transform.parent = gameObject.transform;
                break;
            }
        }
    }
}
