using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AirportStats : MonoBehaviour
{
    TextMeshProUGUI TotalAgents;
    TextMeshProUGUI IncomingAgents;
    TextMeshProUGUI OutgoingAgents;
    TextMeshProUGUI Infected;
    TextMeshProUGUI InfectedPercentage;
    TextMeshProUGUI Susceptible;
    TextMeshProUGUI SusceptiblePercentage;
    TextMeshProUGUI Exposed;
    TextMeshProUGUI ExposedPercentage;
    TextMeshProUGUI VirusInfectiousness;
    TextMeshProUGUI VirusRange;
    TextMeshProUGUI TimeScaleText;
    TextMeshProUGUI TimeElapsed;

    SimulationData sd;
    private void Start()
    {
        sd = FindAnyObjectByType<SimulationData>();

        TotalAgents = gameObject.transform.Find("Airport Stats/Total Agents").gameObject.GetComponent<TextMeshProUGUI>();
        IncomingAgents = gameObject.transform.Find("Airport Stats/Incoming Agents").gameObject.GetComponent<TextMeshProUGUI>();
        OutgoingAgents = gameObject.transform.Find("Airport Stats/Outgoing Agents").gameObject.GetComponent<TextMeshProUGUI>();

        Infected = gameObject.transform.Find("Airport Stats/Infected").gameObject.GetComponent<TextMeshProUGUI>();
        InfectedPercentage = gameObject.transform.Find("Airport Stats/Infected %").gameObject.GetComponent<TextMeshProUGUI>();

        Susceptible = gameObject.transform.Find("Airport Stats/Susceptible").gameObject.GetComponent<TextMeshProUGUI>();
        SusceptiblePercentage = gameObject.transform.Find("Airport Stats/Susceptible %").gameObject.GetComponent<TextMeshProUGUI>();

        Exposed = gameObject.transform.Find("Airport Stats/Exposed").gameObject.GetComponent<TextMeshProUGUI>();
        ExposedPercentage = gameObject.transform.Find("Airport Stats/Exposed %").gameObject.GetComponent<TextMeshProUGUI>();

        VirusInfectiousness = gameObject.transform.Find("Virus Variables/Virus Infectiousness").gameObject.GetComponent<TextMeshProUGUI>();
        VirusInfectiousness.text = VirusInfectiousness.name + ": " + sd.virusData.GetVirusInfectiousness().ToString();

        VirusRange = gameObject.transform.Find("Virus Variables/Virus Exposure Range").gameObject.GetComponent<TextMeshProUGUI>();
        VirusRange.text = VirusRange.name + ": " + sd.virusData.GetInfectionRange().ToString();

        TimeScaleText = gameObject.transform.Find("Virus Variables/Time/TimeNumber").gameObject.GetComponent<TextMeshProUGUI>();
        TimeElapsed = gameObject.transform.Find("Airport Stats/Time Elapsed").gameObject.GetComponent<TextMeshProUGUI>();
    }
    private void FixedUpdate()
    {
        TotalAgents.text = TotalAgents.name + ": " + sd.airportData.GetCurrentNumberOfOutgoingAgents().ToString();
        IncomingAgents.text = IncomingAgents.name + ": " + sd.airportData.GetCurrentNumberOfIncomingAgents().ToString();
        OutgoingAgents.text = OutgoingAgents.name + ": " + sd.airportData.GetCurrentNumberOfOutgoingAgents().ToString();

        Infected.text = Infected.name + ": " + sd.virusData.GetCurrentNumberOfInfected().ToString();
        InfectedPercentage.text = InfectedPercentage.name + ": " + ((float)sd.virusData.GetCurrentNumberOfInfected() / sd.airportData.GetCurrentNumberOfOutgoingAgents() * 100).ToString() + "%";

        Susceptible.text = Susceptible.name + ": " + sd.virusData.GetCurrentNumberOfSusceptible().ToString();
        SusceptiblePercentage.text = SusceptiblePercentage.name + ": " + ((float)sd.virusData.GetCurrentNumberOfSusceptible() / sd.airportData.GetCurrentNumberOfOutgoingAgents() * 100).ToString() + "%";

        Exposed.text = Exposed.name + ": " + sd.virusData.GetCurrentNumberOfExposed().ToString();
        ExposedPercentage.text = ExposedPercentage.name + ": " + ((float)sd.virusData.GetCurrentNumberOfExposed() / sd.airportData.GetCurrentNumberOfOutgoingAgents() * 100).ToString() + "%";

        float TimeScale = (float)gameObject.transform.Find("Virus Variables/Time/TimeSlider").gameObject.GetComponent<Slider>().value;
        TimeScaleText.text = TimeScale.ToString();
        sd.UpdateTimeScale(TimeScale);

        TimeElapsed.text = "Time Elasped: " + (Time.time- sd.StartingTime).ToString() + " s";
    }
}
