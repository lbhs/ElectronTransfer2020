using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagToggle : MonoBehaviour
{
    
    public void toggleTheThimg(){
        GameObject.Find("Lable Canvas").GetComponent<LableManager>().ToggleFlags();
    }
}
