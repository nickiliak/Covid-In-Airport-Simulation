using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AgentVirusData : MonoBehaviour
{
    public enum SEIRMODEL { Susceptible, Exposed, Infected, Recovered }

    [Header("Data")]
    public SEIRMODEL AgentViralState;
    public Color ViralStateColor;

    [Header("Quarantine Measures")]
    public bool MaskWearing;
    public bool SocialDistancing;


    GenerateAgentVirusData VirusDataGen = new();
    SimulationData sd;

    void SetAgentVirusData()
    {
        int StateValue = VirusDataGen.GenerateViralStateValue(sd);

        AgentViralState = (SEIRMODEL)Enum.GetValues(typeof(SEIRMODEL)).GetValue(StateValue);
        ViralStateColor = VirusDataGen.GenerateViralStateColor(StateValue);
        MaskWearing = sd.GetVirusData().GetMaskWearing();
    }


    void EnableMask()
    {
        transform.GetChild(0).gameObject.SetActive(MaskWearing); // Enable or disable mask object
        transform.GetChild(1).gameObject.SetActive(MaskWearing); // Enable or disable glasses
    }

    public void PassSimulationData()
    {
        
        if (sd != null) 
        {
            if (AgentViralState == SEIRMODEL.Susceptible) sd.GetVirusData().IncreaseCurrentNumberOfSusceptible();
            else if(AgentViralState == SEIRMODEL.Infected) sd.GetVirusData().IncreaseCurrentNumberOfInfected();
        }
    }

    private void Start()
    {
        Debug.Log("hey");
        sd = FindAnyObjectByType<SimulationData>();

        SetAgentVirusData();

        GetComponent<Renderer>().material.color = ViralStateColor; //Pass it to the renderer

        EnableMask();

        PassSimulationData();
    }

    //should fix this ugly ass of a function
    public void ChangeViralState(SEIRMODEL newState)
    {
        if (GetComponent<AgentVirusData>().AgentViralState == SEIRMODEL.Susceptible)
            sd.GetVirusData().DecreaseCurrentNumberOfSusceptible();
        else if (GetComponent<AgentVirusData>().AgentViralState == SEIRMODEL.Exposed)
            sd.GetVirusData().DecreaseCurrentNumberOfExposed();
        else if (GetComponent<AgentVirusData>().AgentViralState == SEIRMODEL.Infected)
            sd.GetVirusData().DecreaseCurrentNumberOfInfected();

        AgentViralState = newState;
        if (newState == SEIRMODEL.Susceptible)
            sd.GetVirusData().IncreaseCurrentNumberOfSusceptible();
        else if (newState == SEIRMODEL.Exposed)
        {
            sd.GetVirusData().IncreaseTotalNumberOfExposed();
            sd.GetVirusData().IncreaseCurrentNumberOfExposed();
        }
        else if (newState == SEIRMODEL.Infected)
            sd.GetVirusData().IncreaseCurrentNumberOfInfected();

        SetViralStateColor(VirusDataGen.GenerateViralStateColor(Convert.ToInt32(newState)));
    }

    public void SetViralStateColor(Color newColor)
    {
        ViralStateColor = newColor;
        GetComponent<Renderer>().material.color = ViralStateColor;
    }
}
