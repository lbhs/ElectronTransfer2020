using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MoleculeTeam
{
    public string DisplayName;
    public GameObject Metal;
    public GameObject Ion;
}

public class BattleRoyalAltBuilder : MonoBehaviour //sorry I hard coded to two teams, it defiantly does not make it clean code and should be cleaned up
{

    public MoleculeTeam[] TheTeams;

    [Header("UI Setup")]
    public Dropdown Team1Dropdown;
    public Dropdown Team2Dropdown;

    public Slider IonCountSliderTeam1;
    public Text IonCountTextTeam1;
    public Slider IonCountSliderTeam2;
    public Text IonCountTextTeam2;

    public int MaxIons = 8;
    private int ActualNumberOfIonsTeam1;
    private int PreviousNumberOfIonsTeam1;
    private int ActualNumberOfIonsTeam2;
    private int PreviousNumberOfIonsTeam2;

    public Button StartGameButton;
    private List<GameObject> IonsOnAllTeam1 = new List<GameObject>();
    private List<GameObject> IonsOnAllTeam2 = new List<GameObject>();

    public void BuildSceneTeam1()
    {
        foreach (var item in IonsOnAllTeam1)
        {
            Destroy(item);
        }
        IonsOnAllTeam1.Clear();
        GameObject go;
        //Team 1
        MoleculeTeam Team1 = GetTeam(Team1Dropdown);
        go = GameObject.Instantiate(Team1.Metal, new Vector3(-7.2f, 0, 0), Quaternion.identity);
        IonsOnAllTeam1.Add(go);

        for (int i = 0; i < ActualNumberOfIonsTeam1; i++)
        {
            float num = Mathf.Lerp(4f, -6f, (float)i / (float)ActualNumberOfIonsTeam1);
            go = GameObject.Instantiate(Team1.Ion, new Vector3(-6f, num, 0), Quaternion.identity);
            IonsOnAllTeam1.Add(go);
        }

    }

    public void BuildSceneTeam2()
    {
        foreach (var item in IonsOnAllTeam2)
        {
            Destroy(item);
        }
        IonsOnAllTeam2.Clear();
        GameObject go;
        //Team 2
        MoleculeTeam Team2 = GetTeam(Team2Dropdown);
        go = GameObject.Instantiate(Team2.Metal, new Vector3(11.5f, 0, 0), Quaternion.identity);
        IonsOnAllTeam2.Add(go);

        for (int i = 0; i < ActualNumberOfIonsTeam2; i++)
        {
            float num = Mathf.Lerp(4f, -6f, (float)i / (float)ActualNumberOfIonsTeam2);
            go = GameObject.Instantiate(Team2.Ion, new Vector3(10f, num, 0), Quaternion.identity);
            IonsOnAllTeam2.Add(go);
        }
    }

    void PopulateDropdown(Dropdown dropdown)
    {
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData() { text = "Choose" });
        foreach (var item in TheTeams)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item.DisplayName });
        }
    }


    MoleculeTeam GetTeam(Dropdown dropdown)
    {
        return TheTeams[dropdown.value - 1];
    }

    void Start()
    {
        //Dropdown setup
        PopulateDropdown(Team1Dropdown);
        PopulateDropdown(Team2Dropdown);
    }

    void Update()
    {
        if (IonCountTextTeam1.IsActive())//null error avoidance //Team1
        {
            ActualNumberOfIonsTeam1 = Mathf.CeilToInt(IonCountSliderTeam1.value * MaxIons); //calculate actual number of Ions
            if (PreviousNumberOfIonsTeam1 != ActualNumberOfIonsTeam1)//if it's updated
            {
                IonCountTextTeam1.text = ActualNumberOfIonsTeam1.ToString();//update Text
                BuildSceneTeam1();//rebuild scene
                PreviousNumberOfIonsTeam1 = ActualNumberOfIonsTeam1;
            }
        }
        if (IonCountTextTeam2.IsActive())//null error avoidance //Team2
        {
            ActualNumberOfIonsTeam2 = Mathf.CeilToInt(IonCountSliderTeam2.value * MaxIons); //calculate actual number of Ions
            if (PreviousNumberOfIonsTeam2 != ActualNumberOfIonsTeam2)//if it's updated
            {
                IonCountTextTeam2.text = ActualNumberOfIonsTeam2.ToString();//update Text
                BuildSceneTeam2();//rebuild scene
                PreviousNumberOfIonsTeam2 = ActualNumberOfIonsTeam2;
            }
        }

        if (Team1Dropdown.value != 0 && Team2Dropdown.value != 0)
        {
            StartGameButton.interactable = true;
        }
        else
        {
            StartGameButton.interactable = false;
        }
    }
}
