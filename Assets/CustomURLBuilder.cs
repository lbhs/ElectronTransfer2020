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
                item.options.Add(new Dropdown.OptionData() { text = item2.name });
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
        string url = "https://lbhs.github.io/Games/ElectronTransfer2019/";
        url += "?Particles=";
        foreach (var item in TablePanelDropdowns)
        {
            for (int t = 0; t < PrefabOptions.tiles.Length; t++)
            {
                if(item.options[item.value].text == PrefabOptions.tiles[t].name)
                {
                    url += t + "i";
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
                }
            }
        }
        te.text = url;
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
