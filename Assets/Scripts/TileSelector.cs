using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if UNITY_EDITOR
    using UnityEditor;
# endif
[ExecuteInEditMode]
public class TileSelector : MonoBehaviour
{
    public enum TileTypes { 
        Empty,
        Line ,
        Curve,
        T,
        Cross
        }

    public enum Rotation { 
        North,
        West,
        South,
        East
        }

    public bool isMovable
    {
        get; set;
    }

    public TileTypes tiletype;
    public Rotation rotation;
    
    public GameObject[] Tiles = new GameObject[5];


    private TileTypes _lastType;
    private Rotation _lastRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        isMovable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //DANGER - UPDATING IN EDITOR AS WELL!!!
        if (tiletype != _lastType || rotation != _lastRotation)
        {
            ChangeTileType();
            _lastType = tiletype;
            _lastRotation = rotation;
        }
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
                child.transform.Rotate(0, 90 * (int)rotation, 0, Space.Self);
                child.transform.localPosition = Vector3.zero;
                break;
            }
        }
        EditorUtility.SetDirty(this);
    # endif
    }
  
}
