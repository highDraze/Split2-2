using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
    using UnityEditor;
# endif
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
    public void ChangeTileType()
    { 
    # if UNITY_EDITOR
        for(int i = transform.childCount - 1; i >= 0; --i)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        Debug.Log($"Added a tile of type {tiletype}");

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
                child.transform.localPosition = Vector3.zero;
                break;
            }
        }
    # endif
    }
  
}
