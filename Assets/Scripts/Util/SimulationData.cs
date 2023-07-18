using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationData : MonoBehaviour
{
    public List<GameObject> CurrentIncomingAgents = new List<GameObject>();
    public List<GameObject> CurrentOutgoingAgents = new List<GameObject>();

    public int TotalNumberOfAgents = 0;
    public int NumberOfIncomingAgents = 0;
    public int NumberOfOutgoingAgents = 0;

    public int NumberOfInfected = 0;
    public int NumberOfExposed = 0;
    public int NumberofSusceptible = 0;

    public void InsertNewIncomingAgent(GameObject newAgent)
    {
        CurrentIncomingAgents.Add(newAgent);
        NumberOfIncomingAgents++;
        TotalNumberOfAgents++;
    }

    public void InsertNewOutGoingAgent(GameObject newAgent)
    {
        CurrentIncomingAgents.Add(newAgent);
        NumberOfOutgoingAgents++;
        TotalNumberOfAgents++;
    }
    public void RemoveNewIncomingAgent(GameObject newAgent)
    {
        CurrentIncomingAgents.Remove(newAgent);
        NumberOfIncomingAgents--;
        TotalNumberOfAgents--;
    }

    public void RemoveNewOutGoingAgent(GameObject newAgent)
    {
        CurrentIncomingAgents.Remove(newAgent);
        NumberOfOutgoingAgents--;
        TotalNumberOfAgents--;
    }

    public void IncreaseNumberOfInfected() { NumberOfInfected++; }

    public void IncreaseNumberOfExposed() { NumberOfExposed++; }

    public void IncreaseNumberOfSusceptible() { NumberofSusceptible++; }

    public void DecreaseNumberOfInfected() { NumberOfInfected--; }

    public void DecreaseNumberOfExposed() { NumberOfExposed--; }

    public void DecreaseNumberOfSusceptible() { NumberofSusceptible--; }
}
