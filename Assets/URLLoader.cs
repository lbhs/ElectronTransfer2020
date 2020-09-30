using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class URLLoader : MonoBehaviour
{
    public BuffetTablePossibleParticles list;
    public UIDropToWorld TheBuffetTable;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string[] URLvars = Application.absoluteURL.Split('?')[1].Split('&');
#else
        string url = "https://lbhs.github.io/Games/ElectronTransfer2019/?Particles=0i1i0i1i2i&Scene=0p1c1p2c";
        string[] URLvars = url.Split('?')[1].Split('&');
#endif


        //for the table tiles
        string[] tableParticles = URLvars[0].Split('=')[1].Split('i');
        for (int i = 0; i < tableParticles.Length; i++)
        {
            if (tableParticles[i] != "")
            {
                BuffetTableTiles tileIndex = list.tiles[int.Parse(tableParticles[i])];
                TheBuffetTable.prefabs[i] = tileIndex.prefab;
                TheBuffetTable.Images[i].GetComponent<Image>().sprite = tileIndex.iconImage;
                TheBuffetTable.Images[i].GetComponent<Image>().color = tileIndex.iconColor;
                TheBuffetTable.TitlesOfImages[i].text = tileIndex.name;
                TheBuffetTable.Images[i].SetActive(true);
                TheBuffetTable.TitlesOfImages[i].gameObject.SetActive(true);
            }
        }

        //for the world scene
        string[] worldParticles = URLvars[1].Split('=')[1].Split('c');
        for (int i = 0; i < tableParticles.Length; i++)
        {

        }
    }

    
}
