using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Redox : MonoBehaviour  //this script is attached to all redox active particles (ions and metals)
{
    private forces mainObject;
    private float probability;
    private float tempfactor;
    private Slider temperatureSlider;
    public AudioSource Soundsource;
    public AudioClip Playthis;
    public GameObject KeeperOfListOfIonsToConcentrate;

    public bool isReacting = false;
    [Header("Choose One (choosing none will make this a spectator ion)")]
    public bool isReducingAgent;
    public bool isOxidizingAgent;

    [Rename("Electrode Potential Eº (Volts)")]
    public float EP;

    [Header("This is the particle that should replace the current one when the reaction occurs")]
    public GameObject ReactionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        mainObject = GameObject.Find("GameObject").GetComponent<forces>();
        temperatureSlider = GameObject.Find("temperatureSlider").GetComponent<Slider>();
        Soundsource = GameObject.Find("ETSound").GetComponent<AudioSource>();
        //Playthis = GameObject.Find("Sounds").GetComponent<AudioClip>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Redox>() != null)
        {
            Redox otherP = collision.gameObject.GetComponent<Redox>(); //otherP stands for other particle
            if (otherP.isReducingAgent == true && isOxidizingAgent == true && isReacting == false)
            {
                                
                tempfactor = 5.1f / temperatureSlider.value;
                probability = Random.Range(0.0f, tempfactor);
                //probability factor allows for non-productive collisions between oxidizing agent and reducing agent
                if (probability < EP + otherP.EP || (SceneManager.GetActiveScene().name.Contains("Battle Royal") && EP + otherP.EP > 0)) //if in battle royal scene, 100% chance of rxn
                {
                    //This initiates the reaction and replaces the gameObjects with Prefab to Become
                    isReacting = true;
                    otherP.isReacting = true;
                    print("otherP isReacting");
                    print(gameObject + "is reacting now--shouldn't allow a 2nd rxn");

                    print(gameObject.tag + " has taken electrons from " + otherP.gameObject.tag);

                    if (GameObject.Find("AdjustWaterLevelSlider"))
                    {
                        if (GameObject.Find("AdjustWaterLevelSlider").GetComponent<MoveWaterlineScript>().IonsToConcentrate.Contains(gameObject))  //this adds the newly instantiated ion to the list of IonsToConcentrate
                        {
                            GameObject.Find("AdjustWaterLevelSlider").GetComponent<MoveWaterlineScript>().IonsToConcentrate.Remove(gameObject);  //removes the reacted ion from the list IonsToConcentrate

                        }
                    }
                    

                    //gets positions of both objects
                    Vector3 Rpos = gameObject.transform.position;
                    Vector3 Opos = otherP.transform.position;

                    GameObject NewObject1;
                    GameObject NewObject2;
                    //spawn the new objects with the old coordinates but flipped
                    NewObject1 = Instantiate(otherP.ReactionPrefab, Opos, Quaternion.identity);
                    NewObject2 = Instantiate(ReactionPrefab, Rpos, Quaternion.identity);

                    //Flag management
                    if (otherP.GetComponent<LabelAssigner>().hasFlag == true)
                    {
                        NewObject2.GetComponent<LabelAssigner>().hasFlag = true;
                    }
                    else if (gameObject.GetComponent<LabelAssigner>().hasFlag == true)
                    {
                        NewObject2.GetComponent<LabelAssigner>().hasFlag = true;
                    }

                    //Destroy the old objects
                    gameObject.name = "destroyed";
                    mainObject.gameObjects.Remove(gameObject);  //removes the object to be destroyed from the list of atoms/ions 
                    Destroy(otherP.gameObject);

                    otherP.gameObject.name = "destroyed";
                    mainObject.gameObjects.Remove(otherP.gameObject);
                    Destroy(gameObject);

                    //Plays a sound
                    Soundsource.Play();

                    //The need to rename the gameobject is so that it loses the [P] tag
                    //The tag will automatically re-add the particle to the physics list
                    //If an object is destroyed without being removed from the physics list,
                    //all physics will stop until it is resolved

                }
                else if (EP + otherP.EP < 0)  //gameObject.GetComponent<Redox>().EP != -otherP.GetComponent<Redox>().EP)  
                {
                    print("EP of " + gameObject + "=" + EP);
                    print("denied");
                    //ELSE, PLAY A "DENIED SOUND" SO THAT NON-PRODUCTIVE COLLISIONS CAN BE HEARD!
                    //OFTEN, THIS SOUND PLAYS AFTER A SUCCESSFUL COLLISION???
                    if (GameObject.Find("NoReactionSound") != null)
                    {
                        GameObject.Find("NoReactionSound").GetComponent<AudioSource>().Play();
                    }
                }

            }
        }
    }
    //IEnumerable ReactionDelay() //sometimes it would glitch out and set isReacting to true anyways, this is a quick and dirty fix
    //{
    //    isReacting = true;
    //    yield return new WaitForSeconds(0.01f);
    //}
}
/*
// Update is called once per frame
void Update()
{

}
*/
