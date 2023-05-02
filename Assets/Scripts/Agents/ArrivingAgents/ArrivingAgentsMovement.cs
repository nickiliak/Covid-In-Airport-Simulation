using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArrivingAgentsMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public enum AgentState { None, CheckIn, Restroom, Shop, Eat, Board }
    public AgentState agentState;


    public float ChanceToUseRestroom = 0.33f;
    public float ChanceToCheckIn = 0.7f;
    public float ChanceToShop= 0.8f;
    public float ChanceToEat = 0.2f;
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

    void Start()
    {
        //Initial State
        agentState = AgentState.None;
        Airport = GameObject.Find("Airport");

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
            navMeshAgent.destination = new Vector3(186, 0, 105);
        }
        else if (NeedsCheckIn)
        {
            agentState = AgentState.CheckIn;
            navMeshAgent.destination = new Vector3(241, 0, 43);
            NeedsCheckIn = false;
        }
        else if (NeedsRestroom)
        {
            agentState = AgentState.Restroom;
            navMeshAgent.destination = new Vector3(161, 0, 55);
            NeedsRestroom = false;
        }
        else if (NeedsShop)
        {
            agentState = AgentState.Shop;
            navMeshAgent.destination = new Vector3(109, 0, 56);
            NeedsShop = false;
        }
        else if (NeedsEat)
        {
            agentState = AgentState.Eat;
            navMeshAgent.destination = new Vector3(143, 0, 94);
            NeedsEat = false;
        }
    }
    void ActivateStateBehavior()
    {
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2f)
        {
            if (agentState == AgentState.Board)
            {
                Destroy(gameObject);
            }

            if (agentState == AgentState.CheckIn)
            {

            }

            if (agentState == AgentState.Restroom)
            {

            }

            if (agentState == AgentState.Shop)
            {

            }

            if (agentState == AgentState.Eat)
            {

            }

            agentState = AgentState.None;
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
