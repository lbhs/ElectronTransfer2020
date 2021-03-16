using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

public class URLLoader : MonoBehaviour
{
    public BuffetTablePossibleParticles list;
    public UIDropToWorld TheBuffetTable;
    public bool startUp = true;

#if !UNITY_WEBGL || UNITY_EDITOR
    public static string EditorURL = ""; //"https://interactivechemistry.org/ElectronTransfer2019URLTest?521da424-f960-448e-a734-e7b98787646b";
#endif

    private void Start()
    {
        if (startUp)
        {
            string UUID = GetUUIDFromURL();
            GetUser(UUID, DataScene =>
            {
                print("Building...");
                Build(DataScene);
            });
            //To-Do: get JSON from firebase
            //       parse JSON to DataScene
        }
    }
    public delegate void GetDataSceneCallback(DataScene scene);
    public static void GetUser(string UUID, GetDataSceneCallback callback)
    {
        string databaseURL = CustomURLBuilder.databaseURL;
        RestClient.Get<DataScene>($"{databaseURL}Scenes/{UUID}.json").Then(DataScene =>
        {
            callback(DataScene);
        });
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
            url = "https://interactivechemistry.org/ElectronTransfer2019/?cd7e6e6c-1113-4c8a-9e66-7e23bbd768f7";
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
        for (int i = 0; i < data.ParticlesInScene.Length; i++)
        {
            BuffetTableTiles tileIndex = list.tiles[data.ParticlesInScene[i].ID];
            Instantiate(tileIndex.prefab, new Vector3(data.ParticlesInScene[i].x, data.ParticlesInScene[i].y, 0), tileIndex.prefab.transform.rotation);
        }
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
            ParticleOnSceneClass datadata = new ParticleOnSceneClass(data.ParticlesOnBuffetTable[i], 0, 0);
            TheBuffetTable.Images[i].GetComponent<SceneDataInfo>().data = datadata;
        }

    }
}
