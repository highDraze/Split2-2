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
        Cross,
        O,
        Immovable,
        Treasure,
        Door
        }

    public enum Rotation { 
        North,
        West,
        South,
        East
        }
    [SerializeField]
    public bool isMovable = true;

    public TileTypes tiletype;
    public Rotation rotation;
    
    public GameObject[] Tiles = new GameObject[5];


    private TileTypes _lastType;
    private Rotation _lastRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeTileType();
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
    #if UNITY_EDITOR
        for(int i = transform.childCount - 1; i >= 0; --i)
        {
            if (EditorApplication.isPlaying)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            else
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

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

        #else
            for(int i = transform.childCount - 1; i >= 0; --i)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            foreach(GameObject curObj in Tiles)
            {
                if (curObj.GetComponent<TileProperties>().myType == tiletype)
                {
                    GameObject child = (GameObject)Instantiate(curObj);
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
    #endif
    }
  
}
