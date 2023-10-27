using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static AgentBehavior;
using static ArrivingAgentsBT;
using UnityEngine.AI;
using Unity.VisualScripting.FullSerializer;
using TMPro;
using System.Text.RegularExpressions;

public class BotMiddleBathroom : AgentBehavior
{
    SimulationData sd;
    TextMeshPro AgentCounterNumberText;
    AgentData agentData;

    public BotMiddleBathroom(NavMeshAgent navmeshagent, string keyforBool, AgentData _agentData)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        sd = GameObject.Find("SimulationData").GetComponent<SimulationData>();
        AgentCounterNumberText = GameObject.Find("BotMiddleBathroom Plane/AgentCounter").GetComponent<TextMeshPro>();
        agentData = _agentData;

        positionStrings = new List<string>()
        {
            "Bathroom (0)/Toilets/Toilet" + " (" + Random.Range(0, 6).ToString() + ")" + "/T",
            "Bathroom (0)/Sinks/Sink" + " (" + Random.Range(0, 6).ToString() + ")"
        };

        waitTimes = new List<float>()
        {
            Random.Range(1f, 3f),
            Random.Range(1f, 3f)
        };
    }

    public override NodeState Evaluate()
    {
        if (sd.BotMiddleBathroom_Capacity <= int.Parse(Regex.Match(AgentCounterNumberText.text, @"\d+").Value) && agentData.CurrentAreaInName != "BotMiddleBathroom Plane")
        {
            navmeshAgent.ResetPath();
            parent.SetData(keyForBool, false);
            return NodeState.FAILURE;
        }

        state = RunNextSetInBehavior(false, 2);
        return state;
    }
}
