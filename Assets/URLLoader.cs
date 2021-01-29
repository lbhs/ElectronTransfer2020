using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class URLLoader : MonoBehaviour
{
    public BuffetTablePossibleParticles list;
    public UIDropToWorld TheBuffetTable;
    public bool startUp = true;

#if !UNITY_WEBGL || UNITY_EDITOR
    public static string EditorURL = "";
#endif

    private void Start()
    {
        //if (startUp)
        // Build(GetUUIDFromURL());
    }

    string GetUUIDFromURL()
    {
        string UUID = "";
#if UNITY_WEBGL && !UNITY_EDITOR
                    UUID = Application.absoluteURL.Split('?')[1];

#else
        string url;
        if (EditorURL == "")
        {
            url = "https://lbhs.github.io/Games/ElectronTransfer2019/?48730iuhg";
        }
        else
        {
            url = EditorURL;
        }
        UUID = url.Split('?')[1];
#endif
        return UUID;
    }

    public void Build(DataScene data)
    {
        for (int i = 0; i < data.ParticlesOnBuffetTable.Length; i++)
        {
            BuffetTableTiles tileIndex = list.tiles[data.ParticlesOnBuffetTable[i]];
            TheBuffetTable.prefabs[i] = tileIndex.prefab;
            TheBuffetTable.Images[i].GetComponent<Image>().sprite = tileIndex.iconImage;
            TheBuffetTable.Images[i].GetComponent<Image>().color = tileIndex.iconColor;
            TheBuffetTable.TitlesOfImages[i].text = tileIndex.name;
            TheBuffetTable.Images[i].SetActive(true);
            TheBuffetTable.TitlesOfImages[i].gameObject.SetActive(true);
            TheBuffetTable.Images[i].AddComponent<SceneDataInfo>();
            TheBuffetTable.Images[i].GetComponent<SceneDataInfo>().data.ID = data.ParticlesOnBuffetTable[i];
        }

        for (int i = 0; i < data.ParticlesInScene.Length; i++)
        {
            BuffetTableTiles tileIndex = list.tiles[data.ParticlesInScene[i].ID];
            Instantiate(tileIndex.prefab, new Vector3(data.ParticlesInScene[i].x, data.ParticlesInScene[i].y, 0), tileIndex.prefab.transform.rotation);
        }
    }
}
