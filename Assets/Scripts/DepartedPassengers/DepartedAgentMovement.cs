using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class DepartedAgentMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public enum AgentState {None, Restroom, BaggageClaim, CarRental, ExitAirport }
    public AgentState agentState;

    public float ChanceToUseRestroom = 0.33f;
    public float ChanceToHaveBaggage = 0.8f;
    public float ChanceToWantACar = 0.2f;

    private bool NeedsRestroom = false;
    private bool NeedsBaggage = false;
    private bool NeedsCar = false;

    void Start()
    {
        //Initial State
        agentState = AgentState.None;

        //Randomly make them need to use the bathroom or not
        if (Random.value < ChanceToUseRestroom) NeedsRestroom = true;
        if (Random.value < ChanceToHaveBaggage) NeedsBaggage = true;
        if (Random.value < ChanceToWantACar) NeedsCar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(agentState == AgentState.None) ControlState();
        StateBehavior();
    }

    void ControlState() {
        if (NeedsRestroom)
        {
            agentState = AgentState.Restroom;
            navMeshAgent.destination = new Vector3(75, 0, 56);
            NeedsRestroom = false;
        }
        else if (NeedsBaggage)
        {
            agentState = AgentState.BaggageClaim;
            navMeshAgent.destination = new Vector3(73, 0, 18);
            NeedsBaggage = false;
        }
        else if (NeedsCar)
        {
            agentState = AgentState.CarRental;
            navMeshAgent.destination = new Vector3(96, 0, 36);
            NeedsCar = false;
        }
        else
        {
            agentState = AgentState.ExitAirport;
            navMeshAgent.destination = new Vector3(129, 0, -24);
        }
    }
    void StateBehavior()
    {
        if(Vector3.Distance(transform.position, navMeshAgent.destination) < 2f)
        {
            if(agentState == AgentState.Restroom)
            {

            }

            if (agentState == AgentState.BaggageClaim)
            {

            }

            if (agentState == AgentState.CarRental)
            {

            }

            if (agentState == AgentState.ExitAirport)
            {
                Destroy(gameObject);
            }

            agentState = AgentState.None;
        }
    }
}
