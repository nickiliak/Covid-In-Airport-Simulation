using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationRunning : MonoBehaviour
{
    SimulationData sd;
    public GameObject SE;

    private void OnEnable()
    {
        sd = FindAnyObjectByType<SimulationData>();
        InvokeRepeating(nameof(RecordData), 0f, 1f);
    }
    
    private void RecordData()
    {
        sd.GetRecordedDataGraph().Add(new RecordedData(
            sd.GetVirusData().GetCurrentNumberOfSusceptible(), 
            sd.GetVirusData().GetCurrentNumberOfInfected(), 
            sd.GetVirusData().GetTotalNumberOfExposed(), 
            Time.time - sd.StartingTime));
    }

    private void Update()
    {
        if (sd.TotalFlightsCreated == sd.TotalFlightsInitiated && sd.GetAirportData().GetCurrentTotalNumberOfAgents() == 0)
        {
            sd.EndingTime = Time.time;
            SE.SetActive(true);
        }
    }
}
