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
    TextMeshProUGUI MaxInfectedNumber;
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

        MaxInfectedNumber = gameObject.transform.Find("Agent Variables/Infected/MaxInfectedNumber").gameObject.GetComponent<TextMeshProUGUI>();
    }
    private void FixedUpdate()
    {
        TotalAgents.text = TotalAgents.name + ": " + sd.TotalNumberOfAgents.ToString();
        IncomingAgents.text = IncomingAgents.name + ": " + sd.NumberOfIncomingAgents.ToString();
        OutgoingAgents.text = OutgoingAgents.name + ": " + sd.NumberOfOutgoingAgents.ToString();

        Infected.text = Infected.name + ": " + sd.NumberOfInfected.ToString();
        InfectedPercentage.text = InfectedPercentage.name + ": " + ((float)sd.NumberOfInfected / sd.TotalNumberOfAgents * 100).ToString() + "%";

        Susceptible.text = Susceptible.name + ": " + sd.NumberOfSusceptible.ToString();
        SusceptiblePercentage.text = SusceptiblePercentage.name + ": " + ((float)sd.NumberOfSusceptible / sd.TotalNumberOfAgents * 100).ToString() + "%";

        Exposed.text = Exposed.name + ": " + sd.NumberOfExposed.ToString();
        ExposedPercentage.text = ExposedPercentage.name + ": " + ((float)sd.NumberOfExposed / sd.TotalNumberOfAgents * 100).ToString() + "%";

        int MaximumNumberOfInfected = (int)gameObject.transform.Find("Agent Variables/Infected/InfectedSlider").gameObject.GetComponent<Slider>().value;
        MaxInfectedNumber.text = MaximumNumberOfInfected.ToString();
        sd.UpdateMaximumNumberOfInfected(MaximumNumberOfInfected);

    }
}
