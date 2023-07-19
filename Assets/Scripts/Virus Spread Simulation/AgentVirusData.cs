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
        MaskWearing = sd.MaskWearing;
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
            if (AgentViralState == SEIRMODEL.Susceptible) sd.IncreaseNumberOfSusceptible();
            else if(AgentViralState == SEIRMODEL.Infected) sd.IncreaseNumberOfInfected();
        }
    }

    private void Start()
    {
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
            sd.DecreaseNumberOfSusceptible();
        else if (GetComponent<AgentVirusData>().AgentViralState == SEIRMODEL.Exposed)
            sd.DecreaseNumberOfExposed();
        else if (GetComponent<AgentVirusData>().AgentViralState == SEIRMODEL.Infected)
            sd.DecreaseNumberOfInfected();

        AgentViralState = newState;
        if (newState == SEIRMODEL.Susceptible)
            sd.IncreaseNumberOfSusceptible();
        else if (newState == SEIRMODEL.Exposed)
            sd.IncreaseNumberOfExposed();
        else if (newState == SEIRMODEL.Infected)
            sd.IncreaseNumberOfInfected();

        SetViralStateColor(VirusDataGen.GenerateViralStateColor(Convert.ToInt32(newState)));
    }

    public void SetViralStateColor(Color newColor)
    {
        ViralStateColor = newColor;
        GetComponent<Renderer>().material.color = ViralStateColor;
    }
}
