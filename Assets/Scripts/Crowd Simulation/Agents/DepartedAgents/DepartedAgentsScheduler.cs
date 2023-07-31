using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartedAgentsScheduler : MonoBehaviour
{
    private List<Flight> FlightList;
    private int TotalFlights = 0;
    private float StartingTime = 0f;
    private DepartedAgentSpawner AgentSpawner;

    [Serializable]
    public class Flight
    {
        public float AgentStartArrivingTime;
        public int AgentNumber;
        public int FlightNumber;

        public Flight(float agentStartArrivingTime, int agentNumber, int flightNumber)
        {
            AgentStartArrivingTime = agentStartArrivingTime;
            AgentNumber = agentNumber;
            FlightNumber = flightNumber;
        }
    }
    float GetTimeDelay(float AgentStartArrivingTime)
    {
        return AgentStartArrivingTime - StartingTime;
    }

    public Flight GenerateFlight(float agentStartArrivingTime, int agentNumber)
    {
        TotalFlights++;
        return new Flight(agentStartArrivingTime, agentNumber, TotalFlights);
    }

    public IEnumerator InitiateFlight(Flight flight)
    {
        yield return new WaitForSeconds(GetTimeDelay(flight.AgentStartArrivingTime));
        AgentSpawner.SpawnAgents(flight.AgentNumber, flight.FlightNumber);
    }

    void Start()
    {
        StartingTime = Time.time;
        AgentSpawner = FindObjectOfType<DepartedAgentSpawner>();
    }
}
