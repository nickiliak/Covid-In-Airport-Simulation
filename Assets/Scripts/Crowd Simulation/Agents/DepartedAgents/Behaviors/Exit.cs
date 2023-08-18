using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Exit : AgentBehavior
{
    Vector3 ExitPosition;
    GameObject Agent;
    public Exit(NavMeshAgent navmeshagent, GameObject agent)
    {
        navmeshAgent = navmeshagent;
        Istate = InnerState.EXECUTING;
        Agent = agent;

        ExitPosition = GameObject.Find("Exit/Target").transform.position + new Vector3(Random.Range(-60, 60), 0, 0);

        positionStrings = new List<string>()
        {
           "Exit/Target"
        };

        waitTimes = new List<float>()
        {
            0f
        };
    }

    public override bool ExecuteCustomBehavior()
    {
        Object.Destroy(Agent);
        return true;
    }

    public override NodeState Evaluate()
    {
        if (navmeshAgent.hasPath == false)
        {
            navmeshAgent.destination = ExitPosition;
        }
        else if (IsCloseEnoughToTarget(2))
        {
            Object.Destroy(Agent);
        }

        return state;
    }
}
