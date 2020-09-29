using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

//---------------------
//---------------------
// See the URLVarsExample script to see how to use
//---------------------
//---------------------

public class URLVariable
{
    public string varName;
    public string varValue;
}

/// <summary>
/// A class to get variables in the URL. 
/// See the URLVarsExample script for an example
/// </summary>
public class URLVariableManager : MonoBehaviour
{
    /// <summary>
    /// A list of all URLVariables found in the URL
    /// </summary>
    public static List<URLVariable> ListOfURLVariables = new List<URLVariable>();

    public static string EditorURL;
    private static bool hasUpdated = false;
    private static void updateList()
    {
        hasUpdated = true;

        string url = "";
#if UNITY_WEBGL && !UNITY_EDITOR
        url = Application.absoluteURL;
#else
        url = EditorURL;
#endif


        if (url != "")
        {
            url = url.Split('?')[1];
            ListOfURLVariables.Clear();
            if (url.Contains("&"))
            {
                string[] urls = url.Split('=');
                bool counter = false;
                URLVariable urlv = new URLVariable();
                for (int i = 0; i < urls.Length; i++)
                {
                    if (counter == false)
                    {
                        counter = true;
                        urls = null;
                        urlv = new URLVariable();
                        urlv.varName = urls[i];
                    }
                    else
                    {
                        counter = false;
                        urlv.varName = urls[i];
                        ListOfURLVariables.Add(urlv);
                    }
                }
            }
            else
            {
                string[] urls = url.Split('=');
                URLVariable urlv = new URLVariable();
                urlv.varName = urls[0];
                urlv.varValue = urls[1];
                ListOfURLVariables.Add(urlv);
            }
        }
    }

    /// <summary>
    /// Gets the corresponding variable from the variable's name in the URL
    /// </summary>
    /// <param name="varName"></param>
    /// <returns></returns>
    public static string FindVariable(string varName)
    {
        if(hasUpdated == false)
        {
            updateList();
        }
        List<URLVariable> list = new List<URLVariable>();
        foreach (var item in ListOfURLVariables)
        {
            if(item.varName == varName)
            {
                list.Add(item);
            }
        }
        return list[0].varValue.Replace("%20", " "); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="URL"></param>
    public static void SetEditorURL(string URL)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        EditorURL = URL;
        updateList();
#endif
    }
}
