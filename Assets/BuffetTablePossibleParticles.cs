using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="list",menuName = "BuffetTablePossibleParticles")]
public class BuffetTablePossibleParticles : ScriptableObject
{
    public BuffetTableTiles[] tiles;
}
[System.Serializable]
public class BuffetTableTiles
{
    public  string name;
    public GameObject prefab;
    public Sprite iconImage;
    public Color iconColor;
}
