using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiSeptics : MonoBehaviour
{
    [SerializeField] int ImmunityDuration = 8;

    IEnumerator TemporaryImmunity(Collider other)
    {

        other.GetComponent<AgentVirusData>().naturalSusceptibility = 0;
        yield return new WaitForSeconds(ImmunityDuration);
        other.GetComponent<AgentVirusData>().naturalSusceptibility = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            StartCoroutine(TemporaryImmunity(other));
        }
    }
}
