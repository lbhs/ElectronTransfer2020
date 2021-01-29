using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System;
#if !UNITY_WEBGL || UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif


// Tutorial used: https://medium.com/@rotolonico/firebase-database-in-unity-with-rest-api-42f2cf6a2bbf
public class CustomURLBuilder : MonoBehaviour
{
    //public GameObject[] PrefabOptions;
    public BuffetTablePossibleParticles PrefabOptions;
    public Dropdown[] TablePanelDropdowns;
    //public Dropdown[] SceneItemList;
    //public Dropdown[] SceneItemCount;

    void UpdateUI()
    {
        foreach (var item in TablePanelDropdowns)
        {
            item.ClearOptions();
        }
        foreach (var item in TablePanelDropdowns)
        {
            item.options.Add(new Dropdown.OptionData() { text = "Empty" });
            foreach (var item2 in PrefabOptions.tiles)
            {
                item.options.Add(new Dropdown.OptionData() { text = item2.name/*, image = item2.iconImage*/});
            }
        }

        // foreach (var item in SceneItemList)
        // {
        //     item.ClearOptions();
        // }
        // foreach (var item in SceneItemList)
        // {
        //     item.options.Add(new Dropdown.OptionData() { text = "Empty" });
        //     foreach (var item2 in PrefabOptions.tiles)
        //     {
        //         item.options.Add(new Dropdown.OptionData() { text = item2.name });
        //     }
        // }

    }

    private const string projectId = "electrontransfer2019-default-rtdb"; // You can find this in your Firebase project settings
    public static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";

    public void BuildFakeBuffetTable()
    {
        List<int> TableParticleList = new List<int>();
        bool foundParticles = false;
        foreach (var item in TablePanelDropdowns)
        {
            for (int t = 0; t < PrefabOptions.tiles.Length; t++)
            {
                if (item.options[item.value].text == PrefabOptions.tiles[t].name)
                {
                    TableParticleList.Add(t);
                    // url += t + "i";
                    foundParticles = true;
                }
            }
        }
        GetComponent<URLLoader>().Build(new DataScene(0, TableParticleList.ToArray(), new ParticleOnSceneClass[0]));
    }
    //i'm so sorry for wrighting this bad code, the stuff in the cloud is good though, so a refactor will not be impossible
    public void CopyURLToClipBoard()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string url = Application.absoluteURL.Split('?')[0];
#else
        string url = "https://interactivechemistry.org/ElectronTransfer2019/?cd7e6e6c-1113-4c8a-9e66-7e23bbd768f7".Split('?')[0];
#endif

        List<int> TableParticleList = new List<int>();
        bool foundParticles = false;
        foreach (var item in TablePanelDropdowns)
        {
            for (int t = 0; t < PrefabOptions.tiles.Length; t++)
            {
                if (item.options[item.value].text == PrefabOptions.tiles[t].name)
                {
                    TableParticleList.Add(t);
                    // url += t + "i";
                    foundParticles = true;
                }
            }
        }

        List<ParticleOnSceneClass> particleOnSceneList = new List<ParticleOnSceneClass>();
        GameObject[] FoundParticles = GameObject.FindGameObjectsWithTag("SceneDataObject");
        foreach (var item in FoundParticles)
        {
            particleOnSceneList.Add(item.GetComponent<SceneDataInfo>().data);
        }
        // for (int i = 0; i < SceneItemList.Length; i++)
        // {
        //     for (int t = 0; t < PrefabOptions.tiles.Length; t++)
        //     {
        //         if (SceneItemList[i].options[SceneItemList[i].value].text == PrefabOptions.tiles[t].name)
        //         {
        //             //url += t + "p" + SceneItemCount[i].options[SceneItemCount[i].value].text + "c";
        //             particleOnSceneList.Add(new ParticleOnSceneClass(t, 0, 0));
        //             foundParticles = true;
        //         }
        //     }
        // }
        if (foundParticles == true)
        {      ///-------------------------wraps every thing ug up-----------------
            string NewID;
            NewID = Guid.NewGuid().ToString();
            TimeSpan span = DateTime.Now.Subtract(new DateTime(2021, 1, 1, 0, 0, 0));

            DataScene SceneImBuilding = new DataScene(span.TotalSeconds, TableParticleList.ToArray(), particleOnSceneList.ToArray());

            print(SceneImBuilding);
            RestClient.Put<DataScene>($"{databaseURL}Scenes/{NewID}.json", SceneImBuilding).Then(response =>
            {
                Debug.Log("The Scene was successfully uploaded to the database");

                Application.OpenURL(url + "?" + NewID);

            });

        }

        //TextEditor te = new TextEditor();
        //te.text = url;
        //te.SelectAll();
        //te.Copy();
    }


    private void OnValidate()
    {
        UpdateUI();
    }

    //singelton
    public static CustomURLBuilder instance;

}
