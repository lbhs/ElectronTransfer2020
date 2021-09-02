using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using IC2020;

public class UIDropToWorld : MonoBehaviour, IDropHandler
{
    //  Variable Definitions

    //public bool startWithAllWildCards; // a bool to change all tiles to wild card when standalone scenes are present
    public GameObject[] prefabs; // the list of actual objects to be spawned 
    public GameObject[] possibleParticles; // A list of objects that can be pulled from the buffet table in a specific scene.
    [Header("Ignore:")]
    public GameObject[] Images; // The list of UI elements that are being dragged (need to be labeled 0, 1, 2, etc. in buffet table).
    public Text[] TitlesOfImages;
    public Sprite plus; // Plus symbol overlaid on UI elements.
    public Sprite minus; // Minus symbol overlaid on UI elements.
    public Sprite Water; // Water Sprite
    public Sprite Sphere; // Sphere sprite. Color is controlled by prefab material color.
    public Sprite transparent; // Transparent placeholder image when no other image is used.
    public Sprite WildCardImage; //image for wild card tiles before it was set
    private Vector3 prefabWorldPosition; // Position that the prefab spawns in.
    private int objectToUse; // Index used in prefabs[] to determine which particle is spawned.
    private GameObject MainObject; // "Main" controller gameobject.
                                   // private MoleculeSpawner mSpawner = new MoleculeSpawner(); // A molecule spawner object used to add water molecules.


    public bool SpawnUIInstead = false;

    public void OnDrop(PointerEventData eventData)
    {
        print("starting OnDrop");
        // The buffet table's position
        RectTransform panel = transform as RectTransform;
        prefabWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        prefabWorldPosition.z = 0;

        // the 9 lines below this comment determine whether an should be spawned object depending on whether it is spawned inside or outside of the world.
        
        if (!RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition)) //this boolean is true if object is dropped on the Buffet Table panel, otherwise false
        {
            GameObject UIObject = null;
            foreach (GameObject item in Images)
            {
                if (item.GetComponent<UIDragNDrop>().UseingMe)  //the one item selected from the buffet table is marked as "Useing me" in the UIDragNDrop script
                {
                    UIObject = item;
                    objectToUse = int.Parse(item.name);  //this gets the index of the Prefab to spawn (buffet table indexes prefabs to spawn based on position on the buffet table)
                    print("objectToUse = " + objectToUse);
                }
            }

           
            //if (Images[objectToUse].GetComponent<UIDragNDrop>().useAddShpere == true)
            //{
            //    //if its a wild card, instanciate with custom varibles
            //    Particle p = new Particle(Images[objectToUse].GetComponent<UIDragNDrop>().particleName, Images[objectToUse].GetComponent<UIDragNDrop>().charge, Images[objectToUse].GetComponent<UIDragNDrop>().color, prefabWorldPosition, Images[objectToUse].GetComponent<UIDragNDrop>().mass, Images[objectToUse].GetComponent<UIDragNDrop>().scale, Images[objectToUse].GetComponent<UIDragNDrop>().bounciness, Images[objectToUse].GetComponent<UIDragNDrop>().precipitate, Images[objectToUse].GetComponent<UIDragNDrop>().friction); // Temporary name before a convention is decided on. add friction+perciptates
            //    p.Spawn();
            //    //Debug.Log("nope");
            //}
            if (SpawnUIInstead == false)
            {
                print("ableToSpawn = " + ableToSpawn());
                GameObject NewAtom = Instantiate(prefabs[objectToUse], prefabWorldPosition, Quaternion.identity);
                
                if(NewAtom.tag == "Hydrogen Ion")
                {
                    if (GameObject.Find("AdjustWaterLevelSlider"))
                    {
                        print("recalculate acid concentration now");
                        GameObject.Find("AdjustWaterLevelSlider").GetComponent<MoveWaterlineScript>().IonsToConcentrate.Add(NewAtom);  //this allows the ion to move appropriately when waterline is adjusted

                        GameObject.Find("AcidConcentrationDisplay").GetComponent<AcidConcentrationScript>().CalculateAcidConcentration(-1); //when H+ ions are added, acid concentration goes up!
                    }
                    
                }
               
            }

            else
            {
                if (UIObject != null)
                {
                    print("UIObject " + UIObject);
                    Vector3 oldPos = UIObject.transform.position;
                    Vector3 oldScale = UIObject.transform.localScale;
                    GameObject go = Instantiate(UIObject);
                    Destroy(go.GetComponent<UIDragNDrop>());
                    go.transform.SetParent(GameObject.Find("Canvas").transform);
                    go.transform.position = oldPos;
                    go.transform.localScale = oldScale;
                    go.tag = "SceneDataObject";
                    go.AddComponent<SceneDataInfo>();
                    go.GetComponent<SceneDataInfo>().data.ID = UIObject.GetComponent<SceneDataInfo>().data.ID;
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    go.GetComponent<SceneDataInfo>().data.x = pos.x;
                    go.GetComponent<SceneDataInfo>().data.y = pos.y;
                    
                }
            }

        }
    }


    public bool ableToSpawn()
    {
        print("starting ableToSpawn");
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                if (go.gameObject.transform.parent.gameObject.name == "RollPannelSingle" || go.gameObject.transform.parent.gameObject.name == "RollPannelDouble")
                {
                    return false;
                }


            }
        }

        prefabWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        prefabWorldPosition.z = 0;
        GameObject dummyObject = Instantiate(prefabs[objectToUse], prefabWorldPosition, Quaternion.identity);
        print("instantiated dummyObject =" + dummyObject);
        


        int accuracy = 5; //1 is pixel perfect accuracy but causes stutter, 5 is a great performance but could allow minor overlap
        int range = Screen.height / 2;

        print("poking holes using RayCastPosition");
        for (int x = (int)Input.mousePosition.x - range; x < (int)Input.mousePosition.x + range; x += accuracy)  //checks every 5 pixels to see if there is a "hit"
        {
            for (int y = (int)Input.mousePosition.y - range; y < (int)Input.mousePosition.y + range; y += accuracy)  //the scan covers the entire screen display, looking for double hits!
            {
                Vector2 RayCastPosition = new Vector2(x, y);
                
                RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenPointToRay(new Vector3(x, y, 0)).origin, Vector2.zero);
                //Debug.DrawRay(Camera.main.ScreenPointToRay(new Vector3(x, y, 0)).origin, transform.TransformDirection(Vector3.forward) * 100, Color.green, 10f, false);
                //print("Number of RaycastHit2D hits =" + hits.Length);
                if (hits.Length > 1)
                {

                    foreach (var go in hits)
                    {
                        if (go.rigidbody.gameObject == dummyObject)  //this is now going to check to see if there is a second game object (go2) that is hit with the same ray!
                        {
                            foreach (var go2 in hits)
                            {
                                if (go2.rigidbody.gameObject != dummyObject && (go2.rigidbody.gameObject.GetComponent<DragNDrop>() != null || go2.rigidbody.gameObject.tag == "Diatomic"))
                                {
                                    print("this game object prevented instantiation:" + go2.rigidbody);
                                    Destroy(dummyObject);
                                    Debug.DrawRay(Camera.main.ScreenPointToRay(new Vector3(x, y, 0)).origin, transform.TransformDirection(Vector3.forward) * 100, Color.green, 10f, false);
                                    //GameObject.Find("ConversationDisplay").GetComponent<ConversationDisplayFor2DModels>().DontStackAtoms();
                                    return false;  //there was an overlapping atom, so don't instantiate the new atom
                                }
                            }
                        }
                    }
                }
            }
        }
        Destroy(dummyObject);
        print("destroying the dummy!");
        return true;
    }
}
