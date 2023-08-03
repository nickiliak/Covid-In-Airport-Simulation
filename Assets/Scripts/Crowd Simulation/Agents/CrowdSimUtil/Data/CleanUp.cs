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
                sd.GetAirportData().RemoveNewIncomingAgent(gameObject);
            else
                sd.GetAirportData().RemoveNewOutGoingAgent(gameObject);
            
            if (GetComponent<AgentVirusData>().AgentViralState == AgentVirusData.SEIRMODEL.Susceptible)
                sd.GetVirusData().DecreaseCurrentNumberOfSusceptible();
            else if (GetComponent<AgentVirusData>().AgentViralState == AgentVirusData.SEIRMODEL.Exposed)
                sd.GetVirusData().DecreaseCurrentNumberOfExposed();
            else if (GetComponent<AgentVirusData>().AgentViralState == AgentVirusData.SEIRMODEL.Infected)
                sd.GetVirusData().DecreaseCurrentNumberOfInfected();
        }
    }

    private void OnDestroy()
    {
        RemovePositionFromCDArr();
        RemoveFromSimulationData();
    }
}
