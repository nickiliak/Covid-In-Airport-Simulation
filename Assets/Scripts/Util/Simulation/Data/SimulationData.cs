using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationData : MonoBehaviour
{
    [Header("Simulation Data")]
    public float StartingTime = 0f;
    public float EndingTime = 0f;
    public float TimeScale = 1f;
    public float TotalFlightsCreated = 0f;
    public float TotalFlightsInitiated = 0f;


    public AirportData airportData = new();
    public VirusData virusData = new();
    public List<RecordedData> RData = new();


    [Header("Reducing Capacity ")]
    public int Bathroom1_capacity = 0;


    public void IncreasedTotalFlightsGenerated() { TotalFlightsCreated++; }
    public void IncreasedTotalFlightsInitiated() { TotalFlightsInitiated++; }



    public void UpdateTimeScale(float newTimeScale) { TimeScale = newTimeScale; Time.timeScale = TimeScale; }
    
}
