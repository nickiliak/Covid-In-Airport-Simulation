using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    SimulationData sd;
    private void Start()
    {
        sd = FindAnyObjectByType<SimulationData>();

        TotalAgents = gameObject.transform.Find("Total Agents").gameObject.GetComponent<TextMeshProUGUI>();
        IncomingAgents = gameObject.transform.Find("Incoming Agents").gameObject.GetComponent<TextMeshProUGUI>();
        OutgoingAgents = gameObject.transform.Find("Outgoing Agents").gameObject.GetComponent<TextMeshProUGUI>();

        Infected = gameObject.transform.Find("Infected").gameObject.GetComponent<TextMeshProUGUI>();
        InfectedPercentage = gameObject.transform.Find("Infected %").gameObject.GetComponent<TextMeshProUGUI>();

        Susceptible = gameObject.transform.Find("Susceptible").gameObject.GetComponent<TextMeshProUGUI>();
        SusceptiblePercentage = gameObject.transform.Find("Susceptible %").gameObject.GetComponent<TextMeshProUGUI>();

        Exposed = gameObject.transform.Find("Exposed").gameObject.GetComponent<TextMeshProUGUI>();
        ExposedPercentage = gameObject.transform.Find("Exposed %").gameObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        TotalAgents.text = TotalAgents.name + ": " + sd.TotalNumberOfAgents.ToString();
        IncomingAgents.text = IncomingAgents.name + ": " + sd.NumberOfIncomingAgents.ToString();
        OutgoingAgents.text = OutgoingAgents.name + ": " + sd.NumberOfOutgoingAgents.ToString();

        Infected.text = Infected.name + ": " + sd.NumberOfInfected.ToString();
        InfectedPercentage.text = InfectedPercentage.name + ": " + ((float)sd.NumberOfInfected / sd.TotalNumberOfAgents * 100).ToString() + "%";

        Susceptible.text = Susceptible.name + ": " + sd.NumberofSusceptible.ToString();
        SusceptiblePercentage.text = SusceptiblePercentage.name + ": " + ((float)sd.NumberofSusceptible / sd.TotalNumberOfAgents * 100).ToString() + "%";

        Exposed.text = Exposed.name + ": " + sd.NumberOfExposed.ToString();
        ExposedPercentage.text = ExposedPercentage.name + ": " + ((float)sd.NumberOfExposed / sd.TotalNumberOfAgents * 100).ToString() + "%";

    }
}
