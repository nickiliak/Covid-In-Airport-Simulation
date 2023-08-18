using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SimulationEnd : MonoBehaviour
{
    public GameObject SR;
    SimulationData sd;

    string SimulationRunsPath = "";
    string ParametersFilename = "";
    string graphName = "";
    string barName = "";

    public void WriteParametersUsed()
    {
        TextWriter parameters = new StreamWriter(ParametersFilename, false);
        parameters.WriteLine("Simulation has been executed.");
        parameters.WriteLine("");

        parameters.WriteLine("Simulation Run Number:" + sd.SimulationRunNumber);
        parameters.WriteLine("");

        parameters.WriteLine("Simulation was repeated for " + sd.totalRepeats + " Times.");
        parameters.WriteLine("Total Time elapsed:" + sd.EndingTime);
        parameters.WriteLine("");

        
        parameters.WriteLine("Parameters Used:");
        parameters.WriteLine("");

        parameters.WriteLine("############ AIRPORT ############");
        parameters.WriteLine("");

        ArrivingAgentsScheduler AAS = FindAnyObjectByType<ArrivingAgentsScheduler>();
        parameters.WriteLine("Incoming Total Flights:" + AAS.getTotalFlights());
        for (int i = 0; i < AAS.getFlightList().Count; i++)
        {
            OutgoingFlight currFlight = AAS.getFlightList()[i];
            if (currFlight != null)
                parameters.WriteLine("Flight" + currFlight.FlightNumber.ToString() + ":" +
                    " [Arriving Time: " + currFlight.AgentStartArrivingTime +
                    ", Agent Count: " + currFlight.AgentNumber +
                    ", Boarding Time: " + currFlight.BoardingTime +
                    "]"
                    );
        }
        parameters.WriteLine("");

        DepartedAgentsScheduler DAS = FindAnyObjectByType<DepartedAgentsScheduler>();
        parameters.WriteLine("Outgoing Total Flights:" + DAS.getTotalFlights());
        for (int i = 0; i < DAS.getFlightList().Count; i++)
        {
            IncomingFlight currFlight = DAS.getFlightList()[i];
            if (currFlight != null)
                parameters.WriteLine("Flight" + currFlight.FlightNumber.ToString() + ":" +
                    " [Arriving Time: " + currFlight.AgentStartArrivingTime +
                    ", Agent Count: " + currFlight.AgentNumber +
                    "]"
                    );
        }
        parameters.WriteLine("");

        parameters.WriteLine("############ VIRUS ############");
        parameters.WriteLine("Virus: ["
            + "Virus Infectiousness: " + sd.GetVirusData().GetVirusInfectiousness().ToString() + ","
            + "InfectionRange: " + sd.GetVirusData().GetInfectionRange().ToString() + ","
            + "TotalNumberOfInfected: " + sd.GetVirusData().GetTotalNumberOfInfected().ToString() + "]");
        parameters.WriteLine("MaskWearing: [" + sd.GetVirusData().GetMaskWearing().ToString() + "]");
        parameters.Close();
    }
    public void GenerateData()
    {
        TextWriter graph = new StreamWriter(graphName, false);
        graph.WriteLine("Susceptible,Infected,Exposed,Time");
        for (int i = 0; i < sd.GetRecordedDataGraph().Count; i++)
        {
            graph.WriteLine(sd.GetRecordedDataGraph()[i].GetCurrentNumberOfSuscpetible() + "," +
                sd.GetRecordedDataGraph()[i].GetCurrentNumberOfInfected() + "," +
                sd.GetRecordedDataGraph()[i].GetTotalNumberOfExposed() + "," +
                sd.GetRecordedDataGraph()[i].GetTime());
        }
        graph.Close();

        TextWriter bar = new StreamWriter(barName, false);
        bar.WriteLine("Area,Hit");
        for (int i = 0; i < sd.GetRecordedDataBar().Count; i++)
        {
            bar.WriteLine(sd.GetRecordedDataBar()[i].GetArea() + "," +
                sd.GetRecordedDataBar()[i].GetHit());
        }
        bar.Close();
    }


    private void OnEnable()
    {
        SR.SetActive(false);
        sd = FindAnyObjectByType<SimulationData>();

        ParametersFilename = Application.dataPath 
            + "/GeneratedData/Simulations/Simulation" 
            + sd.SimulationRunNumber.ToString() 
            + "/SimulationParameters.txt";

        graphName = Application.dataPath
            + "/GeneratedData/Simulations/Simulation"
            + sd.SimulationRunNumber.ToString()
            + "/Datasets/Graphs/dataset"
            + sd.currentRepeat.ToString() 
            + "graph.csv";

        barName = Application.dataPath
            + "/GeneratedData/Simulations/Simulation"
            + sd.SimulationRunNumber.ToString()
            + "/Datasets/Bars/dataset" 
            + sd.currentRepeat.ToString() 
            + "bar.csv";

        GenerateData();

        if(sd.currentRepeat < sd.totalRepeats - 1)
        {
            sd.ResetData();
            sd.RestartSim();

            SR.SetActive(true);

            gameObject.SetActive(false);
        }
        else
        {
            WriteParametersUsed();
            Debug.Log("OVER");
            EditorApplication.isPlaying = false;
            Application.Quit();
        }

    }

}
