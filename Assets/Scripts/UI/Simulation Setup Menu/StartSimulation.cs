using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class StartSimulation : MonoBehaviour
{
    private ArrivingAgentsScheduler A_Scheduler;
    private DepartedAgentsScheduler D_Scheduler;

    [SerializeField] Button StartSimulationButton;
    [SerializeField] GameObject IncomingAgentInputFlightFields;
    [SerializeField] GameObject SetUpWindow;

    private List<int> AST_AC_BT = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log(AST_AC_BT[0]);
            Debug.Log(AST_AC_BT[1]);
            Debug.Log(AST_AC_BT[2]);

            A_Scheduler.GenerateFlight(AST_AC_BT[0], AST_AC_BT[1], AST_AC_BT[2]);
            AST_AC_BT.Clear();
        }

        A_Scheduler.InitiateAllFlights();
    }

    void SimulationStart()
    {
        InitiateIncomingAgentsFlights();
        
        SetUpWindow.SetActive(false);
    }
}
