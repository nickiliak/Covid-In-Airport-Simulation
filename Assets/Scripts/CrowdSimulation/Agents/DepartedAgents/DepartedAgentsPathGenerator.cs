using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Factory design pattern for generating the agents behavior for each area
/// </summary>
public class DepartedAgentsPathGenerator 
{
    public List<List<Vector3>> Destinations;
    public List<List<float>> WaitTimes;
    public enum AgentState { None, Restroom, BaggageClaim, CarRental, ExitAirport }
    public List<AgentState> States;

    public GameObject SuitCase;

    private string AgentName;
    void ListsAdd(List<Vector3> destinations, List<float> waitTime, AgentState state)
    {
        Destinations.Add(destinations);
        WaitTimes.Add(waitTime);
        States.Add(state);
    }

    public DepartedAgentsPathGenerator(string name, bool NeedsRestroom = false, bool NeedsBaggage = false, bool NeedsCar = false)
    {
        Destinations = new List<List<Vector3>>();
        WaitTimes = new List<List<float>>();
        States = new List<AgentState>();

        AgentName = name;

        List<Vector3> destinations;
        List<float> waitTime;

        if (NeedsRestroom) { GenerateBehaviorBathroom(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.Restroom); }
        if (NeedsBaggage) { GenerateBehaviorBaggage(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.BaggageClaim); }
        if (NeedsCar) { GenerateBehaviorCar(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.CarRental); }
        GenerateBehaviorExit(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.ExitAirport);
    }

    private void GenerateBehaviorBathroom(out List<Vector3> destinations, out List<float> WaitTime)
    {
        destinations = new List<Vector3>
        {
            GameObject.Find("Bathroom (1)/BathroomBuild (1)/Target").transform.position,
            GameObject.Find("Bathroom (1)/BathroomBuild (1)" + "/Toilet" + " (" + Random.Range(0, 3).ToString() + ")").transform.position,
            GameObject.Find("Bathroom (1)/BathroomBuild (1)" + "/Sink" + " (" + Random.Range(0, 3).ToString() + ")").transform.position
        };

        WaitTime = new List<float>()
        {
            0f,
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    private void GenerateBehaviorBaggage(out List<Vector3> destinations, out List<float> WaitTime)
    {

        SpawnAgentBaggage SpawnBaggage;

        //Pick one of two possible conveyor belts
        if (Random.Range(0, 2) == 0) SpawnBaggage = GameObject.Find("BaggageSpawner1").GetComponent<SpawnAgentBaggage>();
        else SpawnBaggage = GameObject.Find("BaggageSpawner2").GetComponent<SpawnAgentBaggage>();

        //Spawn SuitCase
        SuitCase = SpawnBaggage.SpawnBaggage(AgentName);

        destinations = new List<Vector3>
        {
            GameObject.Find("BaggageClaim/Target").transform.position,
            SuitCase.transform.position
        };

        WaitTime = new List<float>()
        {
            0f,
            Random.Range(1f, 3f)
        };
    }

    private void GenerateBehaviorCar(out List<Vector3> destinations, out List<float> WaitTime)
    {
        destinations = new List<Vector3>
        {
            GameObject.Find("Car Rental/Targets/Target" + " (" + Random.Range(0, 3).ToString() + ")").transform.position
        };

        WaitTime = new List<float>()
        {
            Random.Range(2f, 4f)
        };
    }

    private void GenerateBehaviorExit(out List<Vector3> destinations, out List<float> WaitTime)
    {
        Vector3 ExitPosition = GameObject.Find("Planes/ExitPlane/Target").transform.position;

        destinations = new List<Vector3>
        {
            new Vector3(ExitPosition.x + Random.Range(-70, 70), ExitPosition.y, ExitPosition.z)
        };

        WaitTime = new List<float>()
        {
            0f
        };
    }
}

