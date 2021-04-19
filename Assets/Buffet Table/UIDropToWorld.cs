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
        // The buffet table's position
        RectTransform panel = transform as RectTransform;
        prefabWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        prefabWorldPosition.z = 0;

        // the 9 lines below this comment determine whether an should be spawned object depending on whether it is spawned inside or outside of the world.
        if (!RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition))
        {
            GameObject UIObject = null;
            foreach (GameObject item in Images)
            {
                if (item.GetComponent<UIDragNDrop>().UseingMe)
                {
                    UIObject = item;
                    objectToUse = int.Parse(item.name);
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
                GameObject NewAtom = Instantiate(prefabs[objectToUse], prefabWorldPosition, Quaternion.identity);
                
                if(NewAtom.tag == "Hydrogen Ion")
                {
                    print("recalculate acid concentration now");
                    GameObject.Find("AdjustWaterLevelSlider").GetComponent<MoveWaterlineScript>().IonsToConcentrate.Add(NewAtom);  //this allows the ion to move appropriately when waterline is adjusted
                    
                    GameObject.Find("AcidConcentrationDisplay").GetComponent<AcidConcentrationScript>().CalculateAcidConcentration(); //when H+ ions are added, acid concentration goes up!
                }
               
            }

            else
            {
                if (UIObject != null)
                {
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
}
