using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCounter : MonoBehaviour
{
    private int objectsTouchingFloor = 0;
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
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Agent")) // Change "Player" tag to your object tag
        {
            objectsTouchingFloor--;
            AgentCounterText.text = objectsTouchingFloor.ToString();
        }
    }


}
