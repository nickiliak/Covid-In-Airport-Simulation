using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : AgentBehavior
{
    public Car(NavMeshAgent navmeshagent, string keyforBool)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        positionStrings = new List<string>()
        {
            "Car Rental/Targets/Target" + " (" + Random.Range(0, 3).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            Random.Range(2f, 4f)
        };
    }

    public override NodeState Evaluate()
    {

        state = RunNextSetInBehavior(false, 1);
        return state;
    }
}
