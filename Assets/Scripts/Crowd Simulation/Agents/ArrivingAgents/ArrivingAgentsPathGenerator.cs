using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivingAgentsPathGenerator
{
    public List<List<Vector3>> Destinations;
    public List<List<float>> WaitTimes;
    public enum AgentState { None, CheckIn, Restroom, Shop, Eat, Board }
    public List<AgentState> States;

    public GameObject SuitCase;
    void ListsAdd(List<Vector3> destinations, List<float> waitTime, AgentState state)
    {
        Destinations.Add(destinations);
        WaitTimes.Add(waitTime);
        States.Add(state);
    }

    public ArrivingAgentsPathGenerator(string name, bool NeedsRestroom = false, bool NeedsCheckIn = false, bool NeedsShop = false, bool NeedsEat = false)
    {
        Destinations = new List<List<Vector3>>();
        WaitTimes = new List<List<float>>();
        States = new List<AgentState>();

        List<Vector3> destinations;
        List<float> waitTime;

        if (NeedsRestroom) { GenerateBehaviorBathroom1(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.Restroom); }
        if (NeedsCheckIn) { GenerateBehaviorCheckIn(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.CheckIn); }
        if (NeedsRestroom) { GenerateBehaviorBathroom2(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.Restroom); }
        if (NeedsShop) { GenerateBehaviorShop(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.Shop); }
        if (NeedsEat) { GenerateBehaviorEat(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.Eat); }
        //GenerateBehaviorBoard(out destinations, out waitTime); ListsAdd(destinations, waitTime, AgentState.Board);
    }

    private void GenerateBehaviorBathroom1(out List<Vector3> destinations, out List<float> WaitTime)
    {
        destinations = new List<Vector3>
        {
            GameObject.Find("Bathroom1/BathroomBuild/Target").transform.position,
            GameObject.Find("Bathroom1/BathroomBuild" + "/Toilet" + " (" + Random.Range(0, 3).ToString() + ")").transform.position,
            GameObject.Find("Bathroom1/BathroomBuild" + "/Sink" + " (" + Random.Range(0, 3).ToString() + ")").transform.position
        };

        WaitTime = new List<float>()
        {
            0f,
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    private void GenerateBehaviorCheckIn(out List<Vector3> destinations, out List<float> WaitTime)
    {
        destinations = new List<Vector3>
        {
            GameObject.Find("CheckIn/Targets/" + "Target (" + Random.Range(0,6).ToString() + ")").transform.position
        };

        WaitTime = new List<float>()
        {
            Random.Range(1f, 5f)
        };
    }

    private void GenerateBehaviorBathroom2(out List<Vector3> destinations, out List<float> WaitTime)
    {
        destinations = new List<Vector3>
        {
            GameObject.Find("Bathroom2/BathroomBuild/Target").transform.position,
            GameObject.Find("Bathroom2/BathroomBuild" + "/Toilet" + " (" + Random.Range(0, 3).ToString() + ")").transform.position,
            GameObject.Find("Bathroom2/BathroomBuild" + "/Sink" + " (" + Random.Range(0, 3).ToString() + ")").transform.position
        };

        WaitTime = new List<float>()
        {
            0f,
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    private void GenerateBehaviorShop(out List<Vector3> destinations, out List<float> WaitTime)
    {
        destinations = new List<Vector3>
        {
            GameObject.Find("Shop/ShopBuild/Target").transform.position,
            GameObject.Find("Shop/ShopBuild" + "/Items" + " (" + Random.Range(0, 5).ToString() + ")").transform.position,
            GameObject.Find("Shop/ShopBuild" + "/Target (1)").transform.position
        };

        WaitTime = new List<float>()
        {
            0f,
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    private void GenerateBehaviorEat(out List<Vector3> destinations, out List<float> WaitTime)
    {
        destinations = new List<Vector3>
        {
            GameObject.Find("Food/FoodBuild/Target").transform.position,
            GameObject.Find("Food/FoodBuild/CheckOut").transform.position,
            GameObject.Find("Food/FoodBuild/Chairs" + "/C" + " (" + Random.Range(0, 14) + ")").transform.position
        };

        WaitTime = new List<float>()
        {
            0f,
            Random.Range(1f, 2f),
            Random.Range(1f, 2f)
        };
    }
}
