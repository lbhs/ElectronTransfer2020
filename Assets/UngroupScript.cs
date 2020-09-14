using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UngroupScript : MonoBehaviour
{
    public List<Transform> ObjectsToUnparent = new List<Transform>();
    void Start()
    {
        foreach(Transform child in ObjectsToUnparent)
        {
            child.parent = null; //un-parent all objects
        }
        Destroy(gameObject); //destroy the empty gameobject 
    }
}
