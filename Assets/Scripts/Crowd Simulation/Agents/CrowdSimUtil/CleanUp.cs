using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    void RemovePositionFromCDArr()
    {
        //Remove position from crowd density array 
        AgentData GA = gameObject.GetComponent<AgentData>();
        CrowdDensity CD = FindObjectOfType<CrowdDensity>();
        if (CD != null) CD.ValidPoints[GA.CrowdDensityPos] = 0f;
    }

    void RemoveFromSimulationData()
    {
        SimulationData sd = FindAnyObjectByType<SimulationData>();
        if (sd != null)
        {
            if (GetComponent<AgentData>().AgentType == AgentData.agentType.Incoming)
                sd.RemoveNewIncomingAgent(gameObject);
            else
                sd.RemoveNewOutGoingAgent(gameObject);

            if (GetComponent<AgentVirusData>().AgentViralState == AgentVirusData.SEIRMODEL.Susceptible)
                sd.DecreaseNumberOfSusceptible();
            else if (GetComponent<AgentVirusData>().AgentViralState == AgentVirusData.SEIRMODEL.Exposed)
                sd.DecreaseNumberOfExposed();
            else if (GetComponent<AgentVirusData>().AgentViralState == AgentVirusData.SEIRMODEL.Infected)
                sd.DecreaseNumberOfInfected();
        }
    }

    private void OnDestroy()
    {
        RemovePositionFromCDArr();
        RemoveFromSimulationData();
    }
}
