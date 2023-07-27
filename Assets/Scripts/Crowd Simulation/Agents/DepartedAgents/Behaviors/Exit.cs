using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exit : AgentBehavior
{
    GameObject Agent;
    public Exit(NavMeshAgent navmeshagent, GameObject agent)
    {
        navmeshAgent = navmeshagent;
        Istate = InnerState.EXECUTING;
        Agent = agent;

        Vector3 ExitPosition = GameObject.Find("Exit/Target").transform.position;

        positionStrings = new List<string>()
        {
           "Exit/Target"
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
