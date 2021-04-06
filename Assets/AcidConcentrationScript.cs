using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AcidConcentrationScript : MonoBehaviour
{
    //public List<GameObject> HydrogenIonList;
    public float NumOfHydrogenIons;
    public float AcidConcentration;
    public TMP_Text AcidConcentrationDisplay;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject ion in GameObject.FindGameObjectsWithTag("HydrogenIon"))
        {
            //HydrogenIonList.Add(ion);
            NumOfHydrogenIons++;
        }
        
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        NumOfHydrogenIons = 0;
        foreach (GameObject ion in GameObject.FindGameObjectsWithTag("HydrogenIon"))
        {
            NumOfHydrogenIons++;
        }
        
        AcidConcentration = NumOfHydrogenIons / (3f * GameObject.Find("AdjustWaterLevelSlider").GetComponent<MoveWaterlineScript>().PercentOfOriginalVolume);  //Percent of Original volume = (slider value + 7)/12
        AcidConcentrationDisplay.text = "Acid Concentration = " + AcidConcentration.ToString("n2") + " M";
        
    }

    public void CalculateAcidConcentration()
    {
        NumOfHydrogenIons = 0;
        foreach (GameObject ion in GameObject.FindGameObjectsWithTag("HydrogenIon"))
        {
            NumOfHydrogenIons++;
        }
        AcidConcentration = NumOfHydrogenIons / (3f * GameObject.Find("AdjustWaterLevelSlider").GetComponent<MoveWaterlineScript>().PercentOfOriginalVolume);  //Percent of Original volume = (slider value + 7)/12
        AcidConcentrationDisplay.text = "Acid Concentration = " + AcidConcentration.ToString("n2") + " M";
    }

}
