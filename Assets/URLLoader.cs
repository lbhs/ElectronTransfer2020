using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class URLLoader : MonoBehaviour
{
    public BuffetTablePossibleParticles list;
    public UIDropToWorld TheBuffetTable;

#if !UNITY_WEBGL
    public static string EditorURL="";
#endif

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string[] URLvars = Application.absoluteURL.Split('?')[1].Split('&');
        
#else
        string url;
        if (EditorURL == "")
        {
             url = "https://lbhs.github.io/Games/ElectronTransfer2019/?Particles=0i1i0i1i2i&Scene=0p1c1p2c";
        }
        else
        {
            url = EditorURL;
        }
        string[] URLvars = url.Split('?')[1].Split('&');
#endif


        //for the table tiles
        string[] tableParticles = URLvars[0].Split('=')[1].Split('i');
        for (int i = 0; i < tableParticles.Length; i++)
        {
            print(tableParticles[i]);
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

        //for the world scene 0p1c
        string[] worldParticles = URLvars[1].Split('=')[1].Split('c');
        for (int i = 0; i < worldParticles.Length; i++)
        {
            if (worldParticles[i] != "")
            {
                print(worldParticles[i]);
                string[] thingsToSpawn = worldParticles[i].Split('p');
                if (thingsToSpawn[0] != "")
                {
                    BuffetTableTiles tileIndex = list.tiles[int.Parse(thingsToSpawn[0])];

                    for (int t = 0; t < int.Parse(thingsToSpawn[1]); t++)
                    {
                        Instantiate(tileIndex.prefab, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), tileIndex.prefab.transform.rotation);
                    }
                }
            }
        }
    }

    
}
