using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static AgentBehavior;
using static ArrivingAgentsBT;
using UnityEngine.AI;

public class Bathroom_1 : AgentBehavior
{
    public Bathroom_1(NavMeshAgent navmeshagent, string keyforBool, out AgentAction agentState)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;
        agentState = AgentAction.Restroom;

        positionStrings = new List<string>()
        {
            "Bathroom (0)/Toilets/Toilet" + " (" + Random.Range(0, 6).ToString() + ")" + "/T",
            "Bathroom (0)/Sinks/Sink" + " (" + Random.Range(0, 6).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    public override NodeState Evaluate()
    {

        state = RunNextSetInBehavior(false, 1);
        return state;
    }
}
