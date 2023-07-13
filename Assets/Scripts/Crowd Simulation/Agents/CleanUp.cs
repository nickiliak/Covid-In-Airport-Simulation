using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    void RemovePositionFromCDArr()
    {
        //Remove position from crowd density array 
        GenerateAgentAttributes GA = gameObject.GetComponent<GenerateAgentAttributes>();
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

    private void OnDestroy()
    {
        RemovePositionFromCDArr();

        DecreateGlobalTextCounter();
    }
}