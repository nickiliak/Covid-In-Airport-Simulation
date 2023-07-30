using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Bathroom : AgentBehavior
{
    AgentData agentData;
    TextMesh AgentCounter;
    SimulationData sd;
    public Bathroom(NavMeshAgent navmeshagent, string keyforBool, GameObject Agent)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        agentData = Agent.GetComponent<AgentData>();
        sd = GameObject.Find("SimulationData").GetComponent<SimulationData>();
        AgentCounter = GameObject.Find("Planes/Second Section/Bathroom (1) Plane/AgentCounter").GetComponent<TextMesh>();


        positionStrings = new List<string>()
        {
            "Bathroom (1)/Toilets/Toilet" + " (" + Random.Range(0, 6).ToString() + ")" + "/T",
            "Bathroom (1)/Sinks/Sink" + " (" + Random.Range(0, 6).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    public override NodeState Evaluate()
    {
        if(sd.Bathroom1_capacity <= int.Parse(AgentCounter.text) && agentData.CurrentAreaInName != "Bathroom (1) Plane")
        {            
            navmeshAgent.ResetPath();
            parent.SetData(keyForBool, false);
            return NodeState.FAILURE;
        }

        state = RunNextSetInBehavior(false, 1);
        return state;
    }
}
