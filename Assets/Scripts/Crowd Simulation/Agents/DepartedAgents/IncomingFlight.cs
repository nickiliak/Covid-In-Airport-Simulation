using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingFlight : MonoBehaviour
{
    public float AgentStartArrivingTime;
    public int AgentNumber;
    public int FlightNumber;

    public IncomingFlight(float agentStartArrivingTime, int agentNumber, int flightNumber)
    {
        AgentStartArrivingTime = agentStartArrivingTime;
        AgentNumber = agentNumber;
        FlightNumber = flightNumber;
    }
}
