using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrivingAgentsBT;
using UnityEngine.AI;
using Unity.VisualScripting;

public class WaitingUntilBoard : AgentBehavior
{
    AgentData agentdata;
    bool hasDynamicPosition = false;
    public WaitingUntilBoard(NavMeshAgent navmeshagent, AgentData agentData)
    {
        navmeshAgent = navmeshagent;
        agentdata = agentData;
        Istate = InnerState.EXECUTING;
    }
    
    void GenerateDynamicPosition()
    {
        GameObject Seats = GameObject.Find("Seats");
        agentdata.Seat = Seats.GetComponent<AvailableObjects>().PickRandomAvailableObject();
        if(agentdata.Seat != null) 
            navmeshAgent.destination = agentdata.Seat.transform.position;
            hasDynamicPosition = true;
    }

    public override NodeState Evaluate()
    {
        if (hasDynamicPosition == false) GenerateDynamicPosition();

        return state;
    }
}
