using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;
using static ArrivingAgentsBT;
using System.Text.RegularExpressions;
using TMPro;

public class Shop : AgentBehavior
{
    SimulationData sd;
    TextMeshPro AgentCounterNumberText;
    AgentData agentData;
    public Shop(NavMeshAgent navmeshagent, string keyforBool, AgentData _agentData)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        sd = GameObject.Find("SimulationData").GetComponent<SimulationData>();
        GameObject AgentCounterObject = GameObject.Find("Shop Plane/AgentCounter");
        if (AgentCounterObject != null)
            AgentCounterNumberText = AgentCounterObject.GetComponent<TextMeshPro>();
        else
            AgentCounterNumberText = null;
        agentData = _agentData;

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
        if (AgentCounterNumberText != null && sd.Shop_Capacity <= int.Parse(Regex.Match(AgentCounterNumberText.text, @"\d+").Value) && agentData.CurrentAreaInName != "Shop Plane")
        {
            navmeshAgent.ResetPath();
            parent.SetData(keyForBool, false);
            return NodeState.FAILURE;
        }

        RunNextSetInBehavior(false, 1);
        return state;
    }
}
