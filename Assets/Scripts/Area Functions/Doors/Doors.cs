using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public int AgentCount = 0;
    Animator DoorsAnimator;
    private void Start()
    {
        DoorsAnimator = GetComponent<Animator>();
        DoorsAnimator.SetBool("AgentInRange", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))
        {
            AgentCount++;
            DoorsAnimator.SetBool("AgentInRange", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Agent"))  AgentCount--;
        if(AgentCount == 0) DoorsAnimator.SetBool("AgentInRange", false);

    }
}
