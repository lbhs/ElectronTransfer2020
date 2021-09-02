using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AcidConcentrationScript : MonoBehaviour   //THIS SCRIPT IS ATTACHED TO AcidConcentrationDisplay 
{
    //public List<GameObject> HydrogenIonList;
    public float NumOfHydrogenIons;
    public float AcidConcentration;
    public TMP_Text AcidConcentrationDisplay;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject ion in GameObject.FindGameObjectsWithTag("Hydrogen Ion"))
        {
            //HydrogenIonList.Add(ion);
            NumOfHydrogenIons++;
        }
        
    }

    // Update is called once per frame


    void FixedUpdate()
    {

        CalculateAcidConcentration(0);
        print("fixed update stuff");
       
    }

    public void CalculateAcidConcentration(int offset)
    {
        NumOfHydrogenIons = offset;  //when spawning new H+ ions, there is one extra "Hydrogen Ion" in the list (it's the dummy object), so offset = -1.  Otherwise, offset = 0  THIS IS A BAND-AID!!!
        foreach (GameObject ion in GameObject.FindGameObjectsWithTag("Hydrogen Ion"))
        {
            NumOfHydrogenIons++;       
        }
        //print("Num of H ions =" + NumOfHydrogenIons);
        //print("percent volume = " + GameObject.Find("AdjustWaterLevelSlider").GetComponent<MoveWaterlineScript>().PercentOfOriginalVolume);

        AcidConcentration = NumOfHydrogenIons / (5f * GameObject.Find("AdjustWaterLevelSlider").GetComponent<MoveWaterlineScript>().PercentOfOriginalVolume);  //Percent of Original volume = (slider value + 5)/10
        AcidConcentrationDisplay.text = "Acid Concentration = " + AcidConcentration.ToString("n2") + " M";  //"n2" = 2 decimal places
    }

}
