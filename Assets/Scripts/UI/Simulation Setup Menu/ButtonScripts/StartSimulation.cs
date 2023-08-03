using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class StartSimulation : MonoBehaviour
{
    private SimulationData sd;
    private ArrivingAgentsScheduler A_Scheduler;
    private DepartedAgentsScheduler D_Scheduler;

    [SerializeField] GameObject In_GameMenuWindow;
    [SerializeField] GameObject SimulationRunning;
    [SerializeField] TMP_InputField RepeatForXTimes;

    [Header("Agent Data")]
    [SerializeField] Button StartSimulationButton;
    [SerializeField] GameObject IncomingAgentInputFlightFields;
    [SerializeField] GameObject OutgoingAgentInputFlightFields;
    [SerializeField] GameObject SetUpWindow;

    [Header("Virus Data")]
    [SerializeField] TMP_InputField VirusInfectiousness;
    [SerializeField] TMP_InputField VirusInfectRange;
    [SerializeField] TMP_InputField VirusNoInfected;
    [SerializeField] Toggle MaskWearing;


    private List<int> AST_AC_BT = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        sd = FindObjectOfType<SimulationData>();
        sd.totalRepeats = int.Parse(RepeatForXTimes.text);
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

            sd.OFlights.Add(A_Scheduler.GenerateFlight(AST_AC_BT[0], AST_AC_BT[1], AST_AC_BT[2]));
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
            for (int i = 0; i < 2; i++)
            {
                AST_AC_BT.Add(int.Parse(child.GetChild(i).gameObject.GetComponent<TMP_InputField>().text));
            }

            sd.IFlights.Add(D_Scheduler.GenerateFlight(AST_AC_BT[0], AST_AC_BT[1]));
            AST_AC_BT.Clear();
        }

        D_Scheduler.InitiateAllFlights();
    }
    
    void InitiateSimulationData()
    {
        sd.StartingTime = Time.time;

        sd.GetVirusData().SetTotalNumberOfInfected(int.Parse(VirusNoInfected.text));
        sd.GetVirusData().UpdateInfectionRange(int.Parse(VirusInfectRange.text));
        sd.GetVirusData().UpdateVirusInfectiousness(int.Parse(VirusInfectiousness.text));

        sd.GetVirusData().UpdateMaskWearing(MaskWearing.isOn);
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
