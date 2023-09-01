using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartedAgentsScheduler : MonoBehaviour
{
    private List<IncomingFlight> FlightList;
    private int TotalFlights = 0;
    private float StartingTime = 0f;
    private DepartedAgentSpawner AgentSpawner;
    private SimulationData sd;

    public List<IncomingFlight> getFlightList() { return FlightList; }
    public int getTotalFlights() { return TotalFlights; }
    public float getStartingTime() { return StartingTime; }

    float GetTimeDelay(float AgentStartArrivingTime)
    {
        return AgentStartArrivingTime - StartingTime;
    }

    public void InitiateAllFlights()
    {
        foreach (IncomingFlight flight in FlightList)
        {
            StartCoroutine(InitiateFlight(flight));
        }
    }

    public IncomingFlight GenerateFlight(float agentStartArrivingTime, int agentNumber, int Gate)
    {
        TotalFlights++;
        IncomingFlight newFlight = new(agentStartArrivingTime, agentNumber, TotalFlights, Gate);
        FlightList.Add(newFlight);
        sd.IncreasedTotalFlightsGenerated();
        return newFlight;
    }

    public IEnumerator InitiateFlight(IncomingFlight flight)
    {
        yield return new WaitForSeconds(GetTimeDelay(flight.AgentStartArrivingTime));
        AgentSpawner.SpawnAgents(flight.AgentNumber, flight.FlightNumber, flight.Gate);
        sd.IncreasedTotalFlightsInitiated();
    }

    public void Init()
    {
        StartingTime = Time.time;
        AgentSpawner = FindObjectOfType<DepartedAgentSpawner>();
        FlightList = new List<IncomingFlight>();
        sd = FindAnyObjectByType<SimulationData>();
    }

    public void Reset()
    {
        TotalFlights = 0;
        StartingTime = sd.StartingTime;
    }
}
