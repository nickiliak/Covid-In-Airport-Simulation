using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;
using static ArrivingAgentsBT;

public class Shop : AgentBehavior
{
    public Shop(NavMeshAgent navmeshagent, string keyforBool)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        positionStrings = new List<string>()
        {
            "Shop/ShopBuild/Target",
            "Shop/ShopBuild" + "/Items" + " (" + Random.Range(0, 5).ToString() + ")",
            "Shop/ShopBuild" + "/Target (1)"
        };

        waitTimes = new List<float>()
        {
            0f,
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    public override NodeState Evaluate()
    {
        RunNextSetInBehavior(false, 1);
        return state;
    }
}
