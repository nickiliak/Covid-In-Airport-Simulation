using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    void RemovePositionFromCDArr()
    {
        //Remove position from crowd density array 
        AgentData GA = gameObject.GetComponent<AgentData>();
        CrowdDensity CD = FindObjectOfType<CrowdDensity>();
        if (CD != null) CD.ValidPoints[GA.CrowdDensityPos] = 0f;
    }

    void DecreateGlobalTextCounter()
    {
        GameObject myTextObject = GameObject.Find("AgentCounterText");
        if (myTextObject != null) {
            AgentTextCounter myText = myTextObject.GetComponent<AgentTextCounter>();
            myText.DecreaseAgentCounter();
        }
    }

    void RemoveFromSimulationData()
    {
        //SCurrentIncomingAgents
    }

    private void OnDestroy()
    {
        RemovePositionFromCDArr();

        DecreateGlobalTextCounter();
    }
}
