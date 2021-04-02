using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveWaterlineScript : MonoBehaviour
{
    public Slider WaterlineAdjustSlider;
    public List<GameObject> IonsToConcentrate;
    //public List<float> IonYPositions;
    public GameObject TopWall;
    public float PercentOfOriginalVolume;
    public GameObject WaterlineBackgroundPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        //foreach(GameObject ion in IonsToConcentrate)
        //{
        //    ion.GetComponent<cubeScript>().OriginalYPosition = ion.transform.position.y;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustWaterLevel()
    {
        //Bottom of scene y-position = -8, but wall is at y = -8 and therefore lowest accessible y-value = -7.5 (not counting the radius of the actual ions. . . )
        //Top wall begins at y-position = +5, so highest accessible space is +4.5 
        //NEED TO MOVE TOP WALL according to a slider
        //NEED TO MOVE WATERLINE DOWN TO MATCH THE LOCATION OF TOP WALL
        //NEED TO MOVE PARTICLES (H+ IONS) SO THEY AREN'T ABOVE THE TOP WALL

        TopWall.transform.position = new Vector2(0, WaterlineAdjustSlider.value);  //this moves the wall--easy because the slider is already scaled 0 to 5
        PercentOfOriginalVolume = (WaterlineAdjustSlider.value + 7f) / 12f;
        //print(PercentOfOriginalVolume);

        float WaterlineLevel = 29f * WaterlineAdjustSlider.value +80;  //Algebra based on (0,80);  (5,225);  where first value is world coordinate and 2nd value is canvas coordinate
        WaterlineBackgroundPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, WaterlineLevel);   //225 is the original "height" of the WaterlineBackgroundPanel

        foreach (GameObject ion in IonsToConcentrate)
        {
            //NEED TO CALCULATE Y-POSITION ABOVE THE "BOTTOM".  THEN MULTIPLY THIS VALUE BY PCT SCALING AND THEN RECONVERT TO CARTESIAN COORDINATES
            //Y-POSITION ABOVE BOTTOM = Y-POSITION - BOTTOM POSITION, WHICH IS -8 (1st try)
            //PCT SCALE = (UPPER WALL Y-POS MINUS BOTTOM POSITION)/ORIGINAL HEIGHT, WHICH IS 12
            float IonFullHeight = ion.GetComponent<cubeScript>().OriginalYPosition + 8;  //this is the height above the floor
            float IonTransformYPos = (IonFullHeight * PercentOfOriginalVolume) - 8;  //scaled height with offset of -8 to get proper coordinates relative to 0,0 center of screen
            //print(IonTransformYPos);
            ion.transform.position = new Vector2(ion.transform.position.x, IonTransformYPos);  //this moves each ion in the list to the right y-position        
        }




    }

}
