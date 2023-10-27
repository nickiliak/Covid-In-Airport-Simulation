using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartSimulation : MonoBehaviour
{
    private SimulationData sd;
    private ArrivingAgentsScheduler A_Scheduler;
    private DepartedAgentsScheduler D_Scheduler;

    [SerializeField] GameObject In_GameMenuWindow;
    [SerializeField] GameObject SimulationRunning;
    [SerializeField] TMP_InputField RepeatForXTimes;

    [Header("Agent Data")]
    [SerializeField] UnityEngine.UI.Button StartSimulationButton;
    [SerializeField] GameObject IncomingAgentInputFlightFields;
    [SerializeField] GameObject OutgoingAgentInputFlightFields;
    [SerializeField] GameObject SetUpWindow;

    [Header("Virus Data")]
    [SerializeField] TMP_InputField VirusInfectiousness;
    [SerializeField] TMP_InputField VirusInfectRange;
    [SerializeField] TMP_InputField VirusNoInfected;
    [SerializeField] UnityEngine.UI.Toggle MaskWearing;

    [Header("Area Limit Capacity")]
    [SerializeField] TMP_InputField TopLeftRestroom;
    [SerializeField] TMP_InputField TopMiddleRestroom;
    [SerializeField] TMP_InputField BotMiddleRestroom;
    [SerializeField] TMP_InputField Restaurant;
    [SerializeField] TMP_InputField Shop;


    private List<int> AST_AC_BT = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        sd = FindObjectOfType<SimulationData>();
        StartSimulationButton.onClick.AddListener(SimulationStart); // add the PrintMessage function to the button's onClick event
    }

    void InitiateIncomingAgentsFlights()
    {
        A_Scheduler = gameObject.AddComponent<ArrivingAgentsScheduler>();
        A_Scheduler.Init();

        Transform Fields = IncomingAgentInputFlightFields.transform;
        foreach (Transform child in Fields)
        {
            for (int i = 0; i < 3; i++)
            {
                AST_AC_BT.Add(int.Parse(child.GetChild(i).gameObject.GetComponent<TMP_InputField>().text));
            }

            A_Scheduler.GenerateFlight(AST_AC_BT[0], AST_AC_BT[1], AST_AC_BT[2]);
            AST_AC_BT.Clear();
        }

        A_Scheduler.InitiateAllFlights();
    }

    void InitiateOutgoingAgentFlights()
    {
        D_Scheduler = gameObject.AddComponent<DepartedAgentsScheduler>();
        D_Scheduler.Init();

        Transform Fields = OutgoingAgentInputFlightFields.transform;
        foreach (Transform child in Fields)
        {
            for (int i = 0; i < 3; i++)
            {
                AST_AC_BT.Add(int.Parse(child.GetChild(i).gameObject.GetComponent<TMP_InputField>().text));
            }

            D_Scheduler.GenerateFlight(AST_AC_BT[0], AST_AC_BT[1], AST_AC_BT[2]);
            AST_AC_BT.Clear();
        }

        D_Scheduler.InitiateAllFlights();
    }
    
    void GenerateSimulationFolder()
    {
        sd.SimulationRunNumber = Directory.GetDirectories(Application.dataPath + "/GeneratedData/Simulations").Length;
        string newFolderName = "Simulation" + sd.SimulationRunNumber;
        string newFolderPath = Path.Combine((Application.dataPath + "/GeneratedData/Simulations"), newFolderName);

        // Create the new folder
        Directory.CreateDirectory(newFolderPath);

        string SimulationPath = Application.dataPath + "/GeneratedData/Simulations/Simulation" + sd.SimulationRunNumber.ToString();
        Directory.CreateDirectory(Path.Combine(SimulationPath, "Datasets"));
        Directory.CreateDirectory(Path.Combine(SimulationPath + "/Datasets", "Bars"));
        Directory.CreateDirectory(Path.Combine(SimulationPath + "/Datasets", "Graphs"));
    }

    void InitiateSimulationData()
    {
        sd.StartingTime = Time.time;
        sd.totalRepeats = int.Parse(RepeatForXTimes.text);
        GenerateSimulationFolder();

        for (int i = 0; i < sd.totalRepeats; i++)
        {
            VirusData vD = new VirusData();
            vD.SetTotalNumberOfInfected(int.Parse(VirusNoInfected.text));
            vD.UpdateInfectionRange(int.Parse(VirusInfectRange.text));
            vD.UpdateVirusInfectiousness(int.Parse(VirusInfectiousness.text));
            vD.UpdateMaskWearing(MaskWearing.isOn);

            sd.GetVirusDataList().Add(vD);
        }

        sd.TopLeftBathroom_Capacity = int.Parse(TopLeftRestroom.text);
        sd.TopMiddleBathroom_Capacity = int.Parse(TopMiddleRestroom.text);
        sd.BotMiddleBathroom_Capacity = int.Parse(BotMiddleRestroom.text);
        sd.Restaurant_Capacity = int.Parse(Restaurant.text);
        sd.Shop_Capacity = int.Parse(Shop.text);
    }

    void SimulationStart()
    {
        InitiateSimulationData();
        InitiateIncomingAgentsFlights();
        InitiateOutgoingAgentFlights();
        In_GameMenuWindow.SetActive(true);
        SimulationRunning.SetActive(true);

        SetUpWindow.SetActive(false);
    }
}
