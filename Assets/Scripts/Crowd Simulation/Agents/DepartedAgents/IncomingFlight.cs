using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingFlight
{
    public float AgentStartArrivingTime;
    public int AgentNumber;
    public int FlightNumber;
    public int Gate;

    public IncomingFlight(float agentStartArrivingTime, int agentNumber, int flightNumber, int gate)
    {
        AgentStartArrivingTime = agentStartArrivingTime;
        AgentNumber = agentNumber;
        FlightNumber = flightNumber;
        Gate = gate;
    }
}
