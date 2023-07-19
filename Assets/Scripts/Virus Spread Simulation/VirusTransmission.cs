using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VirusTransmission : MonoBehaviour
{
    AgentVirusData vData;
    private void Start()
    {
        vData = GetComponent<AgentVirusData>();
    }

    float VirusTransmissionProbabilityGen(Vector3 InfectedAgentPosition)
    {
        float distance = Vector3.Distance(InfectedAgentPosition, transform.position);

        if (vData.MaskWearing == true) return (distance);
        else return  distance;
    }
    
    bool InRangeOfInfected(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            AgentVirusData other_vData = other.GetComponent<AgentVirusData>();
            if (other_vData.AgentViralState == AgentVirusData.SEIRMODEL.Infected 
                && vData.AgentViralState == AgentVirusData.SEIRMODEL.Susceptible)
                return true;
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (InRangeOfInfected(other) == true)
        {
            float chance = VirusTransmissionProbabilityGen(other.transform.position); //WHOSE TRANSMISSION CHANCE DO I PASS ? IMPORTANTTTT!!!!!!!!!!!!!!!!!!
            if(Random.value < chance)
            {
                vData.ChangeViralState(AgentVirusData.SEIRMODEL.Exposed);
                Debug.Log("Exposed");
            }
        }
        

    }
}
