using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCounter : MonoBehaviour
{
    public int objectsTouchingFloor = 0;
    TextMesh AgentCounterText;

    void Start()
    {
        GameObject ChildText = transform.Find("AgentCounter").gameObject;
        AgentCounterText = ChildText.GetComponent<TextMesh>();
        AgentCounterText.text = objectsTouchingFloor.ToString();
        AgentCounterText.color = Color.white;
        AgentCounterText.fontStyle= FontStyle.Bold;
        AgentCounterText.fontSize = 30;
        AgentCounterText.richText = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent")) // Change "Player" tag to your object tag
        {
            objectsTouchingFloor++;
            AgentCounterText.text = objectsTouchingFloor.ToString();
            other.gameObject.GetComponent<AgentData>().CurrentAreaInName = gameObject.name;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Agent")) // Change "Player" tag to your object tag
        {
            objectsTouchingFloor--;
            AgentCounterText.text = objectsTouchingFloor.ToString();
            if(other.gameObject.GetComponent<AgentData>().CurrentAreaInName != "GatesArr Plane (1)")
                other.gameObject.GetComponent<AgentData>().CurrentAreaInName = "null";
        }
    }


}
