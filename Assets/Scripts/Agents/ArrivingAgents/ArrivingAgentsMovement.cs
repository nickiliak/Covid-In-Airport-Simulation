using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArrivingAgentsMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public enum AgentState { None, CheckIn, Restroom, Shop, Eat, Board }
    public AgentState agentState;
    public int EntryGateNumber;

    public float ChanceToUseRestroom = 0f;
    public float ChanceToCheckIn = 0f;
    public float ChanceToShop= 0f;
    public float ChanceToEat = 1f;
    public bool TimeToBoard = false;

    private bool NeedsRestroom = false;
    private bool NeedsCheckIn = false;
    private bool NeedsShop = false;
    private bool NeedsEat = false;

    private bool AreaBehaviour = false;
    private bool Waiting = false;
    private List<Vector3> destinations = new List<Vector3>();
    private List<float> WaitTime = new List<float>();

    private GameObject Airport;
    private string FirstSectionAreas = "First Section/Areas";
    private string SecondSectionAreas = "Second Section/Areas";

    ArrStatsData ArrStats;
    void Start()
    {
        //Initial State
        agentState = AgentState.None;
        Airport = GameObject.Find("Airport");

        //Get UI to update it
        ArrStats = GameObject.Find("ArrivingData").GetComponent<ArrStatsData>();
        ArrStats.OnArrAgentCreated();

        //Randomly make them need to use the bathroom or not
        if (Random.value < ChanceToUseRestroom) NeedsRestroom = true;
        if (Random.value < ChanceToCheckIn) NeedsCheckIn = true;
        if (Random.value < ChanceToShop) NeedsShop = true;
        if (Random.value < ChanceToEat) NeedsEat = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agentState == AgentState.None && AreaBehaviour == false) ControlState();
        if (AreaBehaviour == false) ActivateStateBehavior();
        if (AreaBehaviour == true && Waiting == false) StartCoroutine(StateBehavior());
    }

    void ControlState()
    {
        if (TimeToBoard == true)
        {
            agentState = AgentState.Board;
            navMeshAgent.destination = Airport.transform.Find(SecondSectionAreas + "/GatesArr/EntryGates/Gate" + EntryGateNumber.ToString()).position;
        }
        else if (NeedsCheckIn)
        {
            agentState = AgentState.CheckIn;
            NeedsCheckIn = false;
        }
        else if (NeedsRestroom)
        {
            agentState = AgentState.Restroom;
            navMeshAgent.destination = Airport.transform.Find(SecondSectionAreas + "/Bathroom/BathroomBuild/Target").position;
            NeedsRestroom = false;
        }
        else if (NeedsShop)
        {
            agentState = AgentState.Shop;
            navMeshAgent.destination = Airport.transform.Find(SecondSectionAreas + "/Shop/ShopBuild/Target").position;
            NeedsShop = false;
        }
        else if (NeedsEat)
        {
            agentState = AgentState.Eat;
            navMeshAgent.destination = Airport.transform.Find(SecondSectionAreas + "/Food/FoodBuild/Target").position;
            NeedsEat = false;
        }
    }
    void ActivateStateBehavior()
    {
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2f)
        {
            switch (agentState)
            {
                case AgentState.CheckIn:
                    //Randomly Select One Office
                    destinations.Add(Airport.transform.Find(FirstSectionAreas + "/CheckIn/Office" + " (" + Random.Range(0,6).ToString() + ")").position);

                    ArrStats.OnArrAgentCheckIn();
                    navMeshAgent.ResetPath();

                    //Randomly wait a time in each 
                    WaitTime.Add(Random.Range(1f, 5f));
                    break; 

                case AgentState.Restroom:
                    //Randomly select Toilet and sink
                    destinations.Add(Airport.transform.Find(SecondSectionAreas +
                        "/Bathroom/BathroomBuild" + "/Toilet" + " (" + Random.Range(0, 3).ToString() + ")").position);
                    destinations.Add(Airport.transform.Find(SecondSectionAreas +
                        "/Bathroom/BathroomBuild" + "/Sink" + " (" + Random.Range(0, 3).ToString() + ")").position);

                    ArrStats.OnArrAgentRestroom();
                    navMeshAgent.ResetPath();

                    //Randomly wait a time in each 
                    WaitTime.Add(Random.Range(1f, 3f));
                    WaitTime.Add(Random.Range(1f, 3f));
                    break;

                case AgentState.Shop:
                    //Randomly select one of the item stalls and then go to checkout
                    destinations.Add(Airport.transform.Find(SecondSectionAreas +
                        "/Shop/ShopBuild" + "/Items" + " (" + Random.Range(0, 5).ToString() + ")").position);
                    destinations.Add(Airport.transform.Find(SecondSectionAreas +
                        "/Shop/ShopBuild" + "/CheckOut").position);

                    ArrStats.OnArrAgentShop();
                    navMeshAgent.ResetPath();

                    //Randomly wait a time in each 
                    WaitTime.Add(Random.Range(1f, 3f));
                    WaitTime.Add(Random.Range(1f, 3f));
                    break;

                case AgentState.Eat:
                    //Randomly select one of the item stalls and then go to checkout
                    destinations.Add(Airport.transform.Find(SecondSectionAreas +
                        "/Food/FoodBuild/CheckOut").position);
                    destinations.Add(Airport.transform.Find(SecondSectionAreas +
                        "/Food/FoodBuild/Chairs" + "/C" + " (" + Random.Range(0, 14) + ")").position);

                    ArrStats.OnArrAgentEat();
                    navMeshAgent.ResetPath();

                    //Randomly wait a time in each 
                    WaitTime.Add(Random.Range(1f, 2f));
                    WaitTime.Add(Random.Range(1f, 6f));

                    break;

                case AgentState.Board:

                    ArrStats.OnArrAgentDestroyed();
                    Destroy(gameObject);

                    break;

                default:
                    break;
            }

            AreaBehaviour = true;
        }
    }

    IEnumerator StateBehavior()
    {
        if (destinations.Count != 0)
        {
            if (navMeshAgent.hasPath == false)
            {
                navMeshAgent.destination = destinations[0];
            }
            else
            {
                if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2.5f)
                {
                    navMeshAgent.ResetPath();
                    destinations.Remove(destinations[0]);
                    Waiting = true;

                    yield return new WaitForSeconds(WaitTime[0]);
                    WaitTime.Remove(WaitTime[0]);
                    Waiting = false;
                }
            }
        }
        else
        {
            agentState = AgentState.None;
            AreaBehaviour = false;
        }
    }
}
