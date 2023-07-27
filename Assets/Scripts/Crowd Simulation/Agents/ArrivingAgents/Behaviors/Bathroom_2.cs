using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static AgentBehavior;
using static ArrivingAgentsBT;
using UnityEngine.AI;

public class Bathroom_2 : AgentBehavior
{
    public Bathroom_2(NavMeshAgent navmeshagent, string keyforBool)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        positionStrings = new List<string>()
        {
            "Bathroom (2)/Toilets/Toilet" + " (" + Random.Range(0, 6).ToString() + ")" + "/T",
            "Bathroom (2)/Sinks/Sink" + " (" + Random.Range(0, 6).ToString() + ")"
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
