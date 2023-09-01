using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePath : AgentBehavior
{
    int pickUpNo;
    bool HasGeneratedOnce = false;
    GameObject Agent;

    public RandomizePath(UnityEngine.AI.NavMeshAgent navmeshagent, string keyforBool)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        positionStrings = new List<string>()
        {
            "TestTarget" + " (" + Random.Range(0, 30).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            0f
        };
    }

    public override NodeState Evaluate()
    {
        state = RunNextSetInBehavior(true, 1);
        return state;
    }
}
