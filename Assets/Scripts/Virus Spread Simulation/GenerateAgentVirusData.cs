using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AgentVirusData;

public class GenerateAgentVirusData
{
    // IMPORTANT WE DONT GENERATE RECORVERED AGENTS FOR THE TIME BEING OR EXPOSED! SO ONLY S I
    public int GenerateViralStateValue() { if (Random.Range(0, 2) == 0) return 0; else return 2; }           
    public float GenerateTransmissionChance() { return Random.Range(0f, 0.3f); }
    public float GenerateMaskTransmissionStopage() { return Random.Range(0.25f, 0.5f); }

    public bool GenerateMaskWearing() { if (Random.Range(0, 2) == 0) return false; else return true; }
    public Color GenerateViralStateColor(int ViralStateValue) {
        if (ViralStateValue == 0) return Color.blue;
        else if (ViralStateValue == 1) return Color.yellow;
        else if (ViralStateValue == 2) return Color.red;
        else return Color.green;
    }

}
