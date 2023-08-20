using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
    int AgentCount = 0;
    Animator SlidingDoorsAnimator;
    private void Start()
    {
        SlidingDoorsAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))  AgentCount++;
        SlidingDoorsAnimator.SetBool("AgentInRange", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Agent"))  AgentCount--;
        if(AgentCount == 0) SlidingDoorsAnimator.SetBool("AgentInRange", false);

    }
}
