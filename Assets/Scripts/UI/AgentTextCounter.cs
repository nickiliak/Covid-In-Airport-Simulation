using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgentTextCounter : MonoBehaviour
{
    int AgentCounter = 0;
    public void IncreaseAgentCounter()
    {
        AgentCounter++;
        TMP_Text myText = GetComponent<TMP_Text>();
        myText.text = "No. Agents: " + AgentCounter.ToString();
    }

    public void DecreaseAgentCounter()
    {
        AgentCounter--;
        TMP_Text myText = GetComponent<TMP_Text>();
        myText.text = "No. Agents: " + AgentCounter.ToString();
    }

}
