using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeConcentration : MonoBehaviour
{
    public List<GameObject> HydrogenIons;
    public int WaterlinePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void AdjustWaterline()
    {
        //Bottom of scene y-position = -8
        //Top wall begins at y-position = +5
        //NEED TO MOVE TOP WALL according to a slider
        //NEED TO MOVE WATERLINE DOWN TO MATCH THE LOCATION OF TOP WALL
        //NEED TO MOVE PARTICLES (H+ IONS) SO THEY AREN'T ABOVE THE TOP WALL



        foreach(GameObject ion in HydrogenIons)
        {

        }

    }




}
