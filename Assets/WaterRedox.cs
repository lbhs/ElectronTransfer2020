using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterRedox : MonoBehaviour
{
    private forces mainObject;
    private float probability;
    private float tempfactor;
    private Slider temperatureSlider;
    public AudioSource Soundsource;
    public AudioClip Playthis;


    [Header("Choose One (choosing none will make this a spectator ion)")]
    public bool isReducingAgent;
    public bool isOxidizingAgent;

    [Rename("Electrode Potential Eº (Volts)")]
    public float EP;

    [Header("Water has two redox products--H2 gas + oxide ion") ]
    public GameObject ReactionPrefab1;
    public GameObject ReactionPrefab2;

    // Start is called before the first frame update
    void Start()
    {
        mainObject = GameObject.Find("GameObject").GetComponent<forces>();
        if (GameObject.Find("temperatureSlider") != null)
        {
            temperatureSlider = GameObject.Find("temperatureSlider").GetComponent<Slider>();
        }
        Soundsource = GameObject.Find("ETSound").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collider)
    {
        //print(collider.gameObject.tag);

        if (collider.gameObject.tag == "Calcium")
        {
            print("collided with calcium!");
            Vector3 Rpos = gameObject.transform.position;
            Vector3 Opos = collider.transform.position;
            Redox otherP = collider.gameObject.GetComponent<Redox>(); //otherP stands for other particle

            GameObject NewObject1;
            GameObject NewObject2;
            //GameObject NewObject3;

            //spawn the new objects with the old coordinates but flipped
            //NewObject3 = Instantiate(otherP.ReactionPrefab, Opos, Quaternion.identity);
            NewObject1 = Instantiate(ReactionPrefab1, Rpos, Quaternion.identity);
            NewObject2 = Instantiate(ReactionPrefab2, Rpos, Quaternion.identity);
            Soundsource.Play();


            if (otherP.GetComponent<LabelAssigner>().hasFlag == true)
            {
                NewObject1.GetComponent<LabelAssigner>().hasFlag = true;
            }
            //Move the old objects to oblivion
            gameObject.transform.position = new Vector3(1200, 1200, 1200);
            collider.gameObject.transform.position = new Vector3(-500, -500, 0);

            
        }
             
        
  
    }
}

