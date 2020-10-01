using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomURLBuilder : MonoBehaviour
{
    //public GameObject[] PrefabOptions;
    public BuffetTablePossibleParticles PrefabOptions;
    public Dropdown[] TablePanelDropdowns;
    public Dropdown[] SceneItemList;
    public Dropdown[] SceneItemCount;

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

        foreach (var item in SceneItemList)
        {
            item.ClearOptions();
        }
        foreach (var item in SceneItemList)
        {
            item.options.Add(new Dropdown.OptionData() { text = "Empty" });
            foreach (var item2 in PrefabOptions.tiles)
            {
                item.options.Add(new Dropdown.OptionData() { text = item2.name });
            }
        }

    }

    public void CopyURLToClipBoard()
    {
        TextEditor te = new TextEditor();
#if UNITY_WEBGL && !UNITY_EDITOR
        string[] url = Application.absoluteURL.Split('?')[0];
#else
        string url = "https://lbhs.github.io/Games/ElectronTransfer2019/?Particles=0i1i0i1i2i&Scene=0p1c1p2c".Split('?')[0];
#endif
        url += "?Particles=";
        bool foundParticles=false;
        foreach (var item in TablePanelDropdowns)
        {
            for (int t = 0; t < PrefabOptions.tiles.Length; t++)
            {
                if(item.options[item.value].text == PrefabOptions.tiles[t].name)
                {
                    url += t + "i";
                    foundParticles = true;
                }
            }
        }
        url += "&Scene=";
        for (int i = 0; i < SceneItemList.Length; i++)
        {
            for (int t = 0; t < PrefabOptions.tiles.Length; t++)
            {
                if (SceneItemList[i].options[SceneItemList[i].value].text == PrefabOptions.tiles[t].name)
                {
                    url += t + "p" + SceneItemCount[i].options[SceneItemCount[i].value].text +"c";
                    foundParticles = true;
                }
            }
        }
        if (foundParticles == false)
        {
            te.text = url.Split('?')[0];
            te.SelectAll();
            te.Copy();
            return;
        }
        te.text = url;
#if !UNITY_WEBGL
        URLLoader.EditorURL = url;
#endif
    te.SelectAll();
        te.Copy();
    }

    private void OnValidate()
    {
        UpdateUI();
    }

    //singelton
    public static CustomURLBuilder instance;

}
