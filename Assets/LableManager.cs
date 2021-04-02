using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LableManager : MonoBehaviour
{
    public GameObject[] imagePrefabs;
    public GameObject TextPrefab;
    public GameObject FlagPrefab;
    [ContextMenu("test")]


    public void ToggleFlags()
    {
        foreach (Transform item in transform)
        {
            if (item.name == "Flag(Clone)")
            {
                item.gameObject.SetActive(!item.gameObject.activeSelf);
            }
        }
    }
}
