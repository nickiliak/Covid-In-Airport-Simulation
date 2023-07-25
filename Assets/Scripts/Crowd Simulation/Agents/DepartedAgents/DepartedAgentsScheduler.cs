using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartedAgentsScheduler : MonoBehaviour
{
    private int TotalFlights = 0;
    private float StartingTime = 0f;
    private List<Flight> FlightList;
    private DepartedAgentSpawner AgentSpawner;

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

    Flight GenerateFlight(float agentStartArrivingTime, int agentNumber)
    {
        TotalFlights++;
        return new Flight(agentStartArrivingTime, agentNumber, TotalFlights);
    }

    private IEnumerator InitiateFlight(Flight flight)
    {
        yield return new WaitForSeconds(GetTimeDelay(flight.AgentStartArrivingTime));
        //Debug.Log("Flight Initiated");
        AgentSpawner.SpawnAgents(flight.AgentNumber, flight.FlightNumber);
    }

    void Start()
    {
        StartingTime = Time.time;
        FlightList = new List<Flight>();
        AgentSpawner = FindObjectOfType<DepartedAgentSpawner>();

        FlightList.Add(GenerateFlight(StartingTime, 100));
        FlightList.Add(GenerateFlight(StartingTime + 10f, 100));
        FlightList.Add(GenerateFlight(StartingTime + 20f, 100));

        for (int i = 0; i < TotalFlights; i++)
        {
            StartCoroutine(InitiateFlight(FlightList[0]));
            FlightList.Remove(FlightList[0]);
        }

        //Application.Quit();
        //EditorApplication.isPlaying = false;
    }
}
