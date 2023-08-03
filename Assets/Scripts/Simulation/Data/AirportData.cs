using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirportData
{
    private List<GameObject> CurrentIncomingAgents = new List<GameObject>();
    private List<GameObject> CurrentOutgoingAgents = new List<GameObject>();

    private int CurrentTotalNumberOfAgents = 0;
    private int CurrentNumberOfIncomingAgents = 0;
    private int CurrentNumberOfOutgoingAgents = 0;

    public void InsertNewIncomingAgent(GameObject newAgent)
    {
        CurrentIncomingAgents.Add(newAgent);
        CurrentNumberOfIncomingAgents++;
        CurrentTotalNumberOfAgents++;
    }

    public void InsertNewOutGoingAgent(GameObject newAgent)
    {
        CurrentOutgoingAgents.Add(newAgent);
        CurrentNumberOfOutgoingAgents++;
        CurrentTotalNumberOfAgents++;
    }
    public void RemoveNewIncomingAgent(GameObject newAgent)
    {
        CurrentIncomingAgents.Remove(newAgent);
        CurrentNumberOfIncomingAgents--;
        CurrentTotalNumberOfAgents--;
    }

    public void RemoveNewOutGoingAgent(GameObject newAgent)
    {
        CurrentOutgoingAgents.Remove(newAgent);
        CurrentNumberOfOutgoingAgents--;
        CurrentTotalNumberOfAgents--;
    }

    public int GetCurrentTotalNumberOfAgents() { return CurrentTotalNumberOfAgents; }
    public int GetCurrentNumberOfIncomingAgents() { return CurrentNumberOfIncomingAgents; }
    public int GetCurrentNumberOfOutgoingAgents() { return CurrentNumberOfOutgoingAgents; }
}
