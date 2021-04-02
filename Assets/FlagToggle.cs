using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagToggle : MonoBehaviour
{
    private void Start()
    {
        if (!GetComponent<Toggle>().isOn)
        {
            print("Flags are off");
            toggleTheThimg();
        }
    }

    public void toggleTheThimg()
    {
        GameObject.Find("Lable Canvas").GetComponent<LableManager>().ToggleFlags();
    }
}
