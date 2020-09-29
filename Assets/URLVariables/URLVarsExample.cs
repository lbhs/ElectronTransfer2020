using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class URLVarsExample : MonoBehaviour
{
    public Text textBox;

    void Start()
    {
        //--If you are not in a WebGL build, there is no URL--
        //you can use this debug function to set a fake URL:
        URLVariableManager.SetEditorURL("example.com/MyCoolGame?Name=Bob%20Ross&Age=Twenty&Money=20");
        //Don't worry about commenting it out when you build because
        //it will not effect anything if it is a WebGL build

        //-------------------------------------------------------------------------------------------

        //example: this would return "Twenty" from the URL above
        textBox.text = URLVariableManager.FindVariable("Age");

        //example: this would return int 20 from the URL above
        //-note it returns these variables as strings, so you can use float.Parse("string")
        //to get a float if you know the vars will only be these characters . 1 2 3 4 5 6 7 8 9 0
        float money = int.Parse(URLVariableManager.FindVariable("Money"));

        //example: this would return "Bob Ross" from the URL above
        string Name = URLVariableManager.FindVariable("Name");
    }
}
