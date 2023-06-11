using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    public TileSelector.TileTypes myType = TileSelector.TileTypes.Empty;
    // Start is called before the first frame update
    void Start()
    {
        if(myType == TileSelector.TileTypes.Immovable || myType == TileSelector.TileTypes.Treasure)
        {
            gameObject.transform.parent.GetComponent<TileSelector>().isMovable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
