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
    public Boarding(NavMeshAgent navmeshagent, out AgentAction agentState, int EntryGateNumber, GameObject agent)
    {
        navmeshAgent = navmeshagent;
        Agent = agent;
        Istate = InnerState.EXECUTING;
        agentState = AgentAction.Board;

        positionStrings = new List<string>()
        {
            "GatesArr/EntryGates/Gate" + EntryGateNumber.ToString()
        };

        waitTimes = new List<float>()
        {
            0f
        };
    }
    public override void ExecuteCustomBehavior()
    {
        Object.Destroy(Agent);
    }

    public override NodeState Evaluate()
    {

        state = RunNextSetInBehavior(true, 1);
        return state;
    }
}
