using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutgoingFlight
{
    public float AgentStartArrivingTime;
    public float BoardingTime;
    public int AgentNumber;
    public int FlightNumber;

    public OutgoingFlight(float agentStartArrivingTime, int agentNumber, float boardingTime, int flightNumber)
    {
        AgentStartArrivingTime = agentStartArrivingTime;
        AgentNumber = agentNumber;
        BoardingTime = boardingTime;
        FlightNumber = flightNumber;
    }
}
