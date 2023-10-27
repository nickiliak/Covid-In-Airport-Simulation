using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationData : MonoBehaviour
{
    [Header("Simulation Data")]
    public float StartingTime = 0f;
    public float EndingTime = 0f;
    public float TimeScale = 1f;
    public int SimulationRunNumber = 0;

    public float TotalFlightsCreated = 0f;
    public float TotalFlightsInitiated = 0f;

    public int totalRepeats = 0;
    public int currentRepeat = 0;

    public ArrivingAgentsScheduler AAS;
    public DepartedAgentsScheduler DAS;

    private AirportData airportData = new();
    private List<VirusData> virusData = new();
    private List<RecordedData> RDataGraph = new();
    private List<RecordedData> RDataBar = new();

    //Temp counter of infected
    public int counter = 0;

    [Header("Reducing Capacity ")]
    public int TopLeftBathroom_Capacity = 0;
    public int TopMiddleBathroom_Capacity = 0;
    public int BotMiddleBathroom_Capacity = 0;
    public int Restaurant_Capacity = 0;
    public int Shop_Capacity = 0;

    public AirportData GetAirportData() { return airportData; }
    public List<VirusData> GetVirusDataList() { return virusData; }
    public VirusData GetVirusData() { return virusData[currentRepeat]; }
    public List<RecordedData> GetRecordedDataGraph() { return RDataGraph; }
    public List<RecordedData> GetRecordedDataBar() { return RDataBar; }

    public void IncreasedTotalFlightsGenerated() { TotalFlightsCreated++; }
    public void IncreasedTotalFlightsInitiated() { TotalFlightsInitiated++; }

    public void UpdateTimeScale(float newTimeScale) { TimeScale = newTimeScale; Time.timeScale = TimeScale; }

    public void ResetData()
    {
        StartingTime = Time.time;
        EndingTime = 0f;
        TimeScale = 1f;

        TotalFlightsInitiated = 0f;
        currentRepeat++;

        airportData = new();
        RDataGraph = new();
        RDataBar = new();
    }
       
    public void RestartSim()
    {
        AAS = FindAnyObjectByType<ArrivingAgentsScheduler>();
        DAS = FindAnyObjectByType<DepartedAgentsScheduler>();

        AAS.Reset();
        DAS.Reset();

        AAS.InitiateAllFlights();
        DAS.InitiateAllFlights();
    }
}
