using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

public class Eat : AgentBehavior
{
    SimulationData sd;
    TextMeshPro AgentCounterNumberText;
    AgentData agentData;

    public Eat(NavMeshAgent navmeshagent, string keyforBool, AgentData _agentData)
    {
        navmeshAgent = navmeshagent;
        keyForBool = keyforBool;
        Istate = InnerState.EXECUTING;

        sd = GameObject.Find("SimulationData").GetComponent<SimulationData>();
        AgentCounterNumberText = GameObject.Find("Food Plane/AgentCounter").GetComponent<TextMeshPro>();
        agentData = _agentData;

        positionStrings = new List<string>()
        {
            "Food/FoodBuild/Target",
            "Food/FoodBuild/CheckOut" + "/Target" + " (" + Random.Range(0, 9) + ")",
            "Food/FoodBuild/Chairs" + "/C" + " (" + Random.Range(0, 14) + ")"
        };

        waitTimes = new List<float>()
        {
            0f,
            Random.Range(10f, 20f),
            Random.Range(10f, 20f)
        };
    }

    public override bool ExecuteCustomBehavior()
    {

        if(positionStrings.Count == 1)
        {
            // Clear the bit corresponding to the area in the area mask
            navmeshAgent.areaMask &= ~(1 << NavMesh.GetAreaFromName("FoodEntry"));
            navmeshAgent.areaMask |= 1 << NavMesh.GetAreaFromName("FoodExit");
        }

        return false;
    }

    public override NodeState Evaluate()
    {
        if(sd.Restaurant_Capacity <= int.Parse(Regex.Match(AgentCounterNumberText.text, @"\d+").Value) && agentData.CurrentAreaInName != "Food Plane")
        {            
            navmeshAgent.ResetPath();
            parent.SetData(keyForBool, false);
            return NodeState.FAILURE;
        }

        state = RunNextSetInBehavior(true, 1);
        return state;
    }
}
