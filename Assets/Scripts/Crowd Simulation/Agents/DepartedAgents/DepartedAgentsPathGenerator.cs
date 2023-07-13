using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Generating points for the path of the agent
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
            GameObject.Find("Bathroom (1)/Toilets/Toilet" + " (" + Random.Range(0, 6).ToString() + ")" + "/T").transform.position,
            GameObject.Find("Bathroom (1)/Sinks/Sink" + " (" + Random.Range(0, 6).ToString() + ")").transform.position,
        };

        WaitTime = new List<float>()
        {
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    private void GenerateBehaviorBaggage(out List<Vector3> destinations, out List<float> WaitTime)
    {
        int pickUpNo = Random.Range(1, 3);
        SpawnAgentBaggage SpawnBaggage;

        //Pick one of two possible conveyor belts
        if (pickUpNo == 1) SpawnBaggage = GameObject.Find("BaggageSpawner1").GetComponent<SpawnAgentBaggage>();
        else SpawnBaggage = GameObject.Find("BaggageSpawner2").GetComponent<SpawnAgentBaggage>();

        //Spawn SuitCase
        SuitCase = SpawnBaggage.SpawnBaggage(AgentName);
        SuitCase.transform.Rotate(new Vector3(90f, Random.Range(0, 100), Random.Range(0, 100)));

        destinations = new List<Vector3>
        {
            GameObject.Find("BaggageClaim/PickUp (" + pickUpNo.ToString() + ")/Targets/Target" + " (" + Random.Range(0, 40).ToString() + ")" ).transform.position,
        };

        WaitTime = new List<float>()
        {
            Random.Range(8f, 12f)
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
        Vector3 ExitPosition = GameObject.Find("Exit/Target").transform.position;

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
