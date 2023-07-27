using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;
using static ArrivingAgentsBT;

public class CheckIn : AgentBehavior
{
    public CheckIn (NavMeshAgent navmeshagent, string keyforBool)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        positionStrings = new List<string>()
        {
            "CheckIn/Targets/" + "Target (" + Random.Range(0,6).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            Random.Range(1f, 5f)
        };
    }

    public override void ExecuteCustomBehavior()
    {
        navmeshAgent.areaMask |= 1 << NavMesh.GetAreaFromName("AfterCheckIn");
    }

    public override NodeState Evaluate()
    {

        state = RunNextSetInBehavior(true, 1);
        return state;
    }
}
