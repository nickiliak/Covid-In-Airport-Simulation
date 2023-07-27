using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrivingAgentsBT;
using UnityEngine.AI;

public class WaitingUntilBoard : AgentBehavior
{
    public WaitingUntilBoard(NavMeshAgent navmeshagent)
    {
        navmeshAgent = navmeshagent;
        Istate = InnerState.EXECUTING;

        positionStrings = new List<string>()
        {
            "GatesArr/Seat Set (" + Random.Range(0, 6).ToString()
            + ")/Seats (" + Random.Range(0, 2).ToString()
            + ")/Group " + Random.Range(1, 3).ToString()
            + "/Chair (" + Random.Range(0, 5).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            0f
        };
    }

    public override NodeState Evaluate()
    {

        state = RunNextSetInBehavior(false, 1);
        return state;
    }
}
