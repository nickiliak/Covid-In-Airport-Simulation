using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VirusTransmission : MonoBehaviour
{
    SimulationData sd;
    AgentVirusData vData;
    private void Start()
    {
        sd = FindAnyObjectByType<SimulationData>();
        vData = GetComponent<AgentVirusData>();
    }

    float CalculateL(Vector3 InfectedAgentPosition)
    {
        float distance = Vector3.Distance(InfectedAgentPosition, transform.position);
        return (distance - sd.InfectionRange) /  ( - sd.InfectionRange );
    }

    float VirusTransmissionProbabilityGen(Vector3 InfectedAgentPosition)
    {
        float L = CalculateL(InfectedAgentPosition);

        if (sd.MaskWearing) return L / (sd.VirusInfectiousness + sd.VirusInfectiousness * 0.3f);
        else return L / sd.VirusInfectiousness;
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
            float chance = VirusTransmissionProbabilityGen(other.transform.position);
            //Debug.Log(chance);
            if(Random.value < chance)
            {
                vData.ChangeViralState(AgentVirusData.SEIRMODEL.Exposed);
                Debug.Log("Exposed");
            }
        }
    }
}
