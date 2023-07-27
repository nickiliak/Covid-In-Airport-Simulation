using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static AgentBehavior;
using static ArrivingAgentsBT;
using UnityEngine.AI;

public class Eat : AgentBehavior
{
    public Eat(NavMeshAgent navmeshagent, string keyforBool, out AgentAction agentState)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;
        agentState = AgentAction.Eat;

        positionStrings = new List<string>()
        {
            "Food/FoodBuild/Target",
            "Food/FoodBuild/CheckOut",
            "Food/FoodBuild/Chairs" + "/C" + " (" + Random.Range(0, 14) + ")"
        };

        waitTimes = new List<float>()
        {
            0f,
            Random.Range(10f, 20f),
            Random.Range(10f, 20f)
        };
    }

    public override NodeState Evaluate()
    {

        state = RunNextSetInBehavior(false, 1);
        return state;
    }
}
