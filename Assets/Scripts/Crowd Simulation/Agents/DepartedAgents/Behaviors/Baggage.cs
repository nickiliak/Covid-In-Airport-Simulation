using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Baggage : AgentBehavior
{
    public Baggage(NavMeshAgent navmeshagent, string keyforBool, GameObject Agent)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        int pickUpNo = Random.Range(1, 3);
        SpawnAgentBaggage SpawnBaggage;

        //Pick one of two possible conveyor belts
        if (pickUpNo == 1) SpawnBaggage = GameObject.Find("BaggageSpawner1").GetComponent<SpawnAgentBaggage>();
        else SpawnBaggage = GameObject.Find("BaggageSpawner2").GetComponent<SpawnAgentBaggage>();

        //Spawn SuitCase
        SpawnBaggage.SpawnBaggage(Agent.name);
        //SuitCase.transform.Rotate(new Vector3(90f, Random.Range(0, 100), Random.Range(0, 100)));

        positionStrings = new List<string>()
        {
            "BaggageClaim/PickUp (" + pickUpNo.ToString() + ")/Targets/Target" + " (" + Random.Range(0, 40).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            Random.Range(8f, 12f)
        };
    }

    public override NodeState Evaluate()
    {

        state = RunNextSetInBehavior(false, 1);
        return state;
    }
}
