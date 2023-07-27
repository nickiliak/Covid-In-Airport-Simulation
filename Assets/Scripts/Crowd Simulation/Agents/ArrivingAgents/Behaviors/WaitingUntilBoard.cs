using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrivingAgentsBT;
using UnityEngine.AI;
using Unity.VisualScripting;

public class WaitingUntilBoard : AgentBehavior
{
    bool hasDynamicPosition = false;
    public WaitingUntilBoard(NavMeshAgent navmeshagent)
    {
        navmeshAgent = navmeshagent;
        Istate = InnerState.EXECUTING;

        /*positionStrings = new List<string>()
        {
            "GatesArr/Seat Set (" + Random.Range(0, 6).ToString()
            + ")/Seats (" + Random.Range(0, 2).ToString()
            + ")/Group " + Random.Range(1, 3).ToString()
            + "/Chair (" + Random.Range(0, 5).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            0f
        };*/
    }
    //need to changee from string to vector3 fuck my life bro
    void GenerateDynamicPosition()
    {
        GameObject Seats = GameObject.Find("Seats");
        Seats.GetComponent<AvailableObjects>().PickRandomAvailableObject();
        positionStrings = new List<string>()
        {
            Seats.GetComponent<AvailableObjects>().PickRandomAvailableObject().name
        };

        waitTimes = new List<float>()
        {
            0f
        };

        hasDynamicPosition = true;
    }

    public override NodeState Evaluate()
    {
        if (hasDynamicPosition == false) GenerateDynamicPosition();

        state = RunNextSetInBehavior(false, 1);
        return state;
    }
}
