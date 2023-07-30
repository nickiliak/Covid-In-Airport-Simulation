using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Baggage : AgentBehavior
{
    GameObject Agent;
    GameObject SuitCase;
    public Baggage(NavMeshAgent navmeshagent, string keyforBool, GameObject agent)
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
        Agent = agent;
        SuitCase = SpawnBaggage.SpawnBaggage(Agent.name);
        //SuitCase.transform.Rotate(new Vector3(90f, Random.Range(0, 100), Random.Range(0, 100)));

        positionStrings = new List<string>()
        {
            "BaggageClaim/PickUp (" + pickUpNo.ToString() + ")/Targets/Target" + " (" + Random.Range(0, 40).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            0f
        };
    }

    public override bool ExecuteCustomBehavior()
    {
        if (Vector3.Distance(Agent.transform.position, SuitCase.transform.position) < 2)
        {
            Object.Destroy(SuitCase);
            return false;
        }
        else CustomVariableBool2 = true;

        return true;
    }

    public override NodeState Evaluate()
    {

        state = RunNextSetInBehavior(true, 1);
        return state;
    }
}
