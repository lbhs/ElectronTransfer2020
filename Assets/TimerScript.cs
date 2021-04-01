using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TMP_Text TimerDisplayText;
    private int CountingTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountingTime++;
        TimerDisplayText.text = "Time = " + CountingTime / 25;
    }
}
