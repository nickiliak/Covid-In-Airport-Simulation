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
    public float TransmissionChance;
    public float VirusTransmissionRadius;
    public float MaskTransmissionStoppage;

    [Header("Quarantine Measures")]
    public bool MaskWearing;
    public bool SocialDistancing;


    GenerateAgentVirusData VirusDataGen = new();

    void SetAgentVirusData()
    {
        int StateValue = VirusDataGen.GenerateViralStateValue();

        AgentViralState = (SEIRMODEL)Enum.GetValues(typeof(SEIRMODEL)).GetValue(StateValue);
        ViralStateColor = VirusDataGen.GenerateViralStateColor(StateValue);
        MaskTransmissionStoppage = VirusDataGen.GenerateMaskTransmissionStopage();
        TransmissionChance = VirusDataGen.GenerateTransmissionChance();
        MaskWearing = VirusDataGen.GenerateMaskWearing();
    }


    void EnableMask()
    {
        transform.GetChild(0).gameObject.SetActive(MaskWearing); // Enable or disable mask object
        transform.GetChild(1).gameObject.SetActive(MaskWearing); // Enable or disable glasses
    }

    private void Start()
    {
        SetAgentVirusData();

        GetComponent<Renderer>().material.color = ViralStateColor; //Pass it to the renderer

        EnableMask();
    }

    
    public void SetViralStateColor(Color newColor)
    {
        ViralStateColor = newColor;
        GetComponent<Renderer>().material.color = ViralStateColor;
    }
}
