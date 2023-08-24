using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VirusTransmission : MonoBehaviour
{
    SimulationData sd;
    AgentData Data;
    AgentVirusData vData;
    private void Start()
    {
        sd = FindAnyObjectByType<SimulationData>();
        Data = GetComponent<AgentData>();
        vData = GetComponent<AgentVirusData>();
    }

    float CalculateL(Vector3 InfectedAgentPosition)
    {
        float distance = Vector3.Distance(InfectedAgentPosition, transform.position);
        return (distance - sd.GetVirusData().GetInfectionRange()) /  ( - sd.GetVirusData().GetInfectionRange() );
    }

    float VirusTransmissionProbabilityGen(Vector3 InfectedAgentPosition)
    {
        float L = CalculateL(InfectedAgentPosition);

        if (sd.GetVirusData().GetMaskWearing()) return L / (sd.GetVirusData().GetVirusInfectiousness() + sd.GetVirusData().GetVirusInfectiousness() * 0.3f) * vData.naturalSusceptibility;
        else return L / sd.GetVirusData().GetVirusInfectiousness() * vData.naturalSusceptibility;
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

    private IEnumerator AddPointToHeatMap()
    {
        GetComponent<CapsuleCollider>().isTrigger = false;
        yield return new WaitForSeconds(1.0f);
        GetComponent<CapsuleCollider>().isTrigger = true;
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
                sd.GetRecordedDataBar().Add(new RecordedData(Data.CurrentAreaInName, 1));
                StartCoroutine(AddPointToHeatMap());
                Debug.Log("Exposed");
            }
        }
    }
}
