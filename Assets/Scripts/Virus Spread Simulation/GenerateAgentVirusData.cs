using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AgentVirusData;

public class GenerateAgentVirusData
{
    public int GenerateViralStateValue() { return Random.Range(0, 4); }
    public float GenerateTransmissionChance() { return Random.Range(0f, 1f); }
    public bool GenerateMaskWearing() { if (Random.Range(0, 2) == 0) return false; else return true; }
    public Color GenerateViralStateColor(int ViralStateValue) {
        if (ViralStateValue == 0) return Color.blue;
        else if (ViralStateValue == 1) return Color.yellow;
        else if (ViralStateValue == 2) return Color.red;
        else return Color.green;
    }

}
