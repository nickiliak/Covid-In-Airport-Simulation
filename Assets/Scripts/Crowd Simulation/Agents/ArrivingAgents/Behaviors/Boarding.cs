using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static AgentBehavior;
using static ArrivingAgentsBT;
using UnityEngine.AI;

public class Boarding : AgentBehavior
{
    GameObject Agent;
    AgentData agentData;
    Vector3 FinalDestination;

    public Boarding(NavMeshAgent navmeshagent, int EntryGateNumber, GameObject agent)
    {
        navmeshAgent = navmeshagent;
        Agent = agent;
        agentData = Agent.GetComponent<AgentData>();
        Istate = InnerState.EXECUTING;

        FinalDestination = GameObject.Find("GatesArr/EntryGates/Gate" + EntryGateNumber.ToString() + "/GateDoor/Target").transform.position;
        positionStrings = new List<string>()
        {
            "GatesArr/EntryGates/Gate" + EntryGateNumber.ToString() + "/GateDoor/Target"
        };

        waitTimes = new List<float>()
        {
            0f
        };
    }
    public override bool ExecuteCustomBehavior()
    {
        if (navmeshAgent.destination != FinalDestination)
        {
            navmeshAgent.areaMask |= 1 << NavMesh.GetAreaFromName("AfterCheckIn");
            navmeshAgent.destination = FinalDestination;
        }

        if(Vector3.Distance(Agent.transform.position, FinalDestination) < 2)
            Object.Destroy(Agent);
        return true;
    }

    public override NodeState Evaluate()
    {
        if (Agent.GetComponent<ArrivingAgentsBT>().TimeToBoard == false) 
            return NodeState.FAILURE;
        else 
            state = RunNextSetInBehavior(true, 1);
        
        return state;
    }
}
