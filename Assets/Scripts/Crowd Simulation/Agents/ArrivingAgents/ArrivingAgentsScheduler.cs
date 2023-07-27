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

    Flight GenerateFlight(float agentStartArrivingTime, int agentNumber, float boardingTime)
    {
        TotalFlights++;
        return new Flight(agentStartArrivingTime, agentNumber, boardingTime, TotalFlights);
    }

    private IEnumerator InitiateFlight(Flight flight)
    {
        yield return new WaitForSeconds(GetTimeDelay(flight.AgentStartArrivingTime));
        //Debug.Log("Flight Initiated");
        AgentSpawner.SpawnAgents(flight.AgentNumber, flight.BoardingTime, flight.FlightNumber);
    }

    void Start()
    {
        StartingTime = Time.time;
        FlightList = new List<Flight>();
        AgentSpawner = FindObjectOfType<ArrivingAgentsSpawner>();

        FlightList.Add(GenerateFlight(StartingTime, 60, 80f));
        FlightList.Add(GenerateFlight(StartingTime, 45, 100f));
        FlightList.Add(GenerateFlight(StartingTime, 69, 120f));

        FlightList.Add(GenerateFlight(StartingTime, 60, 80f));
        FlightList.Add(GenerateFlight(StartingTime, 45, 100f));
        FlightList.Add(GenerateFlight(StartingTime, 69, 120f));

        for (int i = 0; i < TotalFlights; i++)
        {
            StartCoroutine(InitiateFlight(FlightList[0]));
            FlightList.Remove(FlightList[0]);
        }

        //Application.Quit();
        //EditorApplication.isPlaying = false;
    }
}
