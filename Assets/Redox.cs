using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Redox : MonoBehaviour  //this script is attached to all redox active particles (ions and metals)
{
    private forces mainObject;
    private float probability;
    private float tempfactor;
    private Slider temperatureSlider;
    public AudioSource Soundsource;
    public AudioClip Playthis;
    public GameObject KeeperOfListOfIonsToConcentrate;
    public TMP_Text ConversationDisplay;

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
                                
                tempfactor = 12f / temperatureSlider.value;  //initial slider setting is temperature value = 8      12/8 = 1.5  100% change Mg reacts with H+, about 50% chance zinc reacts with H+
                probability = Random.Range(0.0f, tempfactor);
                //probability factor allows for non-productive collisions between oxidizing agent and reducing agent
                //print("tempfactor =" +tempfactor);
                //print("probability =" +probability);
                if (probability < EP + otherP.EP || (SceneManager.GetActiveScene().name.Contains("Battle Royal") && EP + otherP.EP > 0)) //if in battle royal scene, 100% chance of rxn
                {
                    //This initiates the reaction and replaces the gameObjects with Prefab to Become
                    isReacting = true;
                    otherP.isReacting = true;
                    //print("otherP isReacting");
                    //print(gameObject + "is reacting now--shouldn't allow a 2nd rxn");

                    if (SceneManager.GetActiveScene().name.Contains("Battle Royal"))  
                    {
                        print(gameObject.tag + " has taken electrons from " + otherP.gameObject.tag);  //Redox species are tagged specifically as Ion or Metal
                        GameObject.Find("ConversationDisplayTMPro").GetComponent<TextMeshProUGUI>().text = (gameObject.tag + " has taken electrons from " + otherP.gameObject.tag + "!\n" + gameObject.tag.ToString().Replace("Ion", string.Empty) + "wins this Battle!!");
                        print(gameObject.tag.ToString().Replace("Ion", string.Empty));  //This truncates the "Ion" part of the gameObject's tag to simplify naming of the winner!

                       if(Camera.main.GetComponent<Animator>() != null)  //This part of the code displays the Instant Replay used in the Battle Royal scene.
                        {
                            //move replay button down 
                            GameObject.Find("SeeReplayButton").GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -120);
                            //find animation objects 
                            Transform AnimationObjectOxidation = Camera.main.transform.Find("AnimationObjectOxidation").transform;
                            Transform AnimationObjectReducting = Camera.main.transform.Find("AnimationObjectReducting").transform;
                            Transform AnimationObjectReductingResult = Camera.main.transform.Find("AnimationObjectReductingResult").transform;
                            Transform AnimationObjectOxidationResult = Camera.main.transform.Find("AnimationObjectOxidationResult").transform;

                            //reset objects in amiantion 
                            Transform[] AnimObjs = new Transform[] { AnimationObjectOxidation, AnimationObjectReducting, AnimationObjectReductingResult, AnimationObjectOxidationResult };
                            foreach (var _transform in AnimObjs)
                            {
                                foreach (Transform child in _transform)
                                {
                                    if(child.name != "Flag Sprite")
                                    {
                                        Destroy(child.gameObject);
                                    }
                                }
                            }

                            //set new objects in animation 
                            GameObject g1 =Instantiate(gameObject, Vector3.zero, Quaternion.identity, AnimationObjectOxidation);
                            GameObject g2 = Instantiate(otherP.gameObject, Vector3.zero, Quaternion.identity, AnimationObjectReducting);
                            GameObject g3 = Instantiate(ReactionPrefab, Vector3.zero, Quaternion.identity, AnimationObjectOxidationResult);
                            GameObject g4 = Instantiate(otherP.ReactionPrefab,Vector3.zero, Quaternion.identity, AnimationObjectReductingResult);

                            GameObject[] gobjs = new GameObject[] { g1, g2, g3, g4 };

                            //put text objects
                            foreach (var go in gobjs)
                            {
                                print("text");
                                GameObject text = Instantiate(Camera.main.transform.Find("TextAnimationCanvas").gameObject, Vector3.zero, Quaternion.identity, go.transform);
                                text.SetActive(true);
                                if (go.tag.Contains("Metal"))
                                {
                                    text.GetComponentInChildren<TMP_Text>().text = go.tag;
                                }
                                else
                                {
                                    text.GetComponentInChildren<TMP_Text>().text = go.GetComponent<LabelAssigner>().Lable;
                                }
                            }

                            //only have the mesh/materuial, no scripts 
                            foreach (var go in gobjs)
                            {
                                foreach (var comp in go.GetComponents<Component>())
                                {
                                    if(!(comp is Transform) && !(comp is MeshRenderer) && !(comp is MeshFilter))
                                    {
                                        Destroy(comp);
                                    }
                                }
                                var type = System.Type.GetType("JustStayAtZeroZeroZero");
                                go.AddComponent(type);
                            }
                        }
                    }

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

                    //Soundsource Plays the ETSound (a GameObject that must be added to the scene and defined in the inspector)
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
