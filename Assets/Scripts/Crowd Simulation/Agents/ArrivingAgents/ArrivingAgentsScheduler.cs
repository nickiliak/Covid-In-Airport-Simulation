using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrivingAgentsScheduler : MonoBehaviour
{
    private int TotalFlights = 0;
    private float StartingTime = 0f;
    private List<Flight> FlightList;
    private ArrivingAgentsSpawner AgentSpawner;

    [Serializable]
    public class Flight
    {
        public float AgentStartArrivingTime;
        public float BoardingTime;
        public int AgentNumber;
        public int FlightNumber;

        public Flight(float agentStartArrivingTime, int agentNumber, float boardingTime, int flightNumber)
        {
            AgentStartArrivingTime = agentStartArrivingTime;
            AgentNumber = agentNumber;
            BoardingTime = boardingTime;
            FlightNumber = flightNumber;
        }
    }
    float GetTimeDelay(float AgentStartArrivingTime)
    {
        return AgentStartArrivingTime - StartingTime;
    }
    public void InitiateAllFlights()
    {
        foreach (Flight flight in FlightList)
        {
            StartCoroutine(InitiateFlight(flight));
        }
    }

    public void GenerateFlight(float agentStartArrivingTime, int agentNumber, float boardingTime)
    {
        TotalFlights++;
        FlightList.Add(new Flight(agentStartArrivingTime, agentNumber, boardingTime, TotalFlights));
    }
    private IEnumerator InitiateFlight(Flight flight)
    {
        yield return new WaitForSeconds(GetTimeDelay(flight.AgentStartArrivingTime));
        AgentSpawner.SpawnAgents(flight.AgentNumber, flight.BoardingTime, flight.FlightNumber);
    }
    public void Init()
    {
        StartingTime = Time.time;
        AgentSpawner = FindObjectOfType<ArrivingAgentsSpawner>();
        FlightList = new List<Flight>();
    }
}
