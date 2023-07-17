using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AgentVirusData : MonoBehaviour
{
    public enum SEIRMODEL { Susceptible, Infected, Exposed, Recovered }

    [Header("Agent Virus Data")]
    public SEIRMODEL AgentViralState;
    public Color ViralStateColor;
    public float TransmissionChance;
    public bool MaskWearing;
    

    GenerateAgentVirusData VirusDataGen = new();
    
    private void Start()
    {
        SetAgentVirusData();
        EnableMask();
    }

    void SetAgentVirusData()
    {
        int StateValue = VirusDataGen.GenerateViralStateValue();

        AgentViralState = (SEIRMODEL)Enum.GetValues(typeof(SEIRMODEL)).GetValue(StateValue);
        ViralStateColor = VirusDataGen.GenerateViralStateColor(StateValue);
        TransmissionChance = VirusDataGen.GenerateTransmissionChance();
        MaskWearing = VirusDataGen.GenerateMaskWearing();
    }

    void EnableMask()
    {
        transform.GetChild(0).gameObject.SetActive(MaskWearing); // Enable or disable mask object
    }
}
