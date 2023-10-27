using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrivingAgentsScheduler : MonoBehaviour
{
    private int TotalFlights = 0;
    private float StartingTime = 0f;
    private List<OutgoingFlight> FlightList;
    private ArrivingAgentsSpawner AgentSpawner;
    private SimulationData sd;

    public int getTotalFlights() { return TotalFlights; }
    public float getStartingTime() { return StartingTime; }
    public List<OutgoingFlight> getFlightList() { return FlightList; }

    float GetTimeDelay(float AgentStartArrivingTime)
    {
        return AgentStartArrivingTime - StartingTime;
    }
    public void InitiateAllFlights()
    {
        foreach (OutgoingFlight flight in FlightList)
        {
            StartCoroutine(InitiateFlight(flight));
        }
    }

    public OutgoingFlight GenerateFlight(float agentStartArrivingTime, int agentNumber, float boardingTime)
    {
        TotalFlights++;
        OutgoingFlight newFLight = new(agentStartArrivingTime, agentNumber, boardingTime, TotalFlights);
        FlightList.Add(newFLight);
        sd.IncreasedTotalFlightsGenerated();
        return newFLight;
    }
    private IEnumerator InitiateFlight(OutgoingFlight flight)
    {
        yield return new WaitForSeconds(GetTimeDelay(flight.AgentStartArrivingTime));
        AgentSpawner.SpawnAgents(flight.AgentNumber, sd.StartingTime + flight.BoardingTime, flight.FlightNumber);
        sd.IncreasedTotalFlightsInitiated();
    }
    public void Init()
    {
        StartingTime = Time.time;
        AgentSpawner = FindObjectOfType<ArrivingAgentsSpawner>();
        FlightList = new List<OutgoingFlight>();
        sd = FindAnyObjectByType<SimulationData>();
        sd.StartingTime = StartingTime;
    }

    public void Reset()
    {
        TotalFlights = 0;
        StartingTime = sd.StartingTime;
    }

}
