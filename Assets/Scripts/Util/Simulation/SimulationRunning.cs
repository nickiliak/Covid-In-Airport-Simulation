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
        sd.RData.Add(new RecordedData(
            sd.virusData.GetCurrentNumberOfSusceptible(), 
            sd.virusData.GetCurrentNumberOfInfected(), 
            sd.virusData.GetTotalNumberOfExposed(), 
            Time.time - sd.StartingTime));
    }

    private void Update()
    {
        if (sd.TotalFlightsCreated == sd.TotalFlightsInitiated && sd.airportData.GetCurrentTotalNumberOfAgents() == 0)
        {
            sd.EndingTime = Time.time;
            SE.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
