using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class TopLeftBathroom : AgentBehavior
{
    SimulationData sd;
    TextMeshPro AgentCounterNumberText;
    AgentData agentData;

    public TopLeftBathroom(NavMeshAgent navmeshagent, string keyforBool, AgentData _agentData)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        sd = GameObject.Find("SimulationData").GetComponent<SimulationData>();
        GameObject AgentCounterObject = GameObject.Find("TopLeftBathroom Plane/AgentCounter");
        if(AgentCounterObject != null )
            AgentCounterNumberText = AgentCounterObject.GetComponent<TextMeshPro>();
        else 
            AgentCounterNumberText = null;

        agentData = _agentData;

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
        if (AgentCounterNumberText != null && sd. TopLeftBathroom_Capacity <= int.Parse(Regex.Match(AgentCounterNumberText.text, @"\d+").Value) && agentData.CurrentAreaInName != "TopLeftBathroom Plane")
        {
            navmeshAgent.ResetPath();
            parent.SetData(keyForBool, false);
            return NodeState.FAILURE;
        }

        state = RunNextSetInBehavior(false, 1);
        return state;
    }
}
