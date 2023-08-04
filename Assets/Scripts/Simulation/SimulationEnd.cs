using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SimulationEnd : MonoBehaviour
{
    public GameObject SR;
    SimulationData sd;

    string ParametersFilename = "";
    string graphName = "";
    string barName = "";

    public void GenerateData()
    {
        TextWriter parameters = new StreamWriter(ParametersFilename, false);
        parameters.WriteLine("Simulation has been executed.");
        parameters.WriteLine("Total Time elapsed:" + sd.EndingTime);
        parameters.WriteLine("");
        parameters.WriteLine("Parameters Used:");
        parameters.WriteLine("Virus: [" 
            + "Virus Infectiousness: " + sd.GetVirusData().GetVirusInfectiousness().ToString() + "," 
            + "InfectionRange: " + sd.GetVirusData().GetInfectionRange().ToString() + ","
            + "TotalNumberOfInfected: " + sd.GetVirusData().GetTotalNumberOfInfected().ToString() + "]");
        parameters.WriteLine("MaskWearing: [" + sd.GetVirusData().GetMaskWearing().ToString() + "]");
        parameters.Close();

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
        ParametersFilename = Application.dataPath + "/DataGeneration/AirportSimulation_Parameters.txt";
        graphName = Application.dataPath + "/DataGeneration/Datasets/Graphs/dataset" + sd.currentRepeat.ToString() + "graph.csv";
        barName = Application.dataPath + "/DataGeneration/Datasets/Bars/dataset" + sd.currentRepeat.ToString() + "bar.csv";

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
            Debug.Log("OVER");
            EditorApplication.isPlaying = false;
            Application.Quit();
        }

    }

}
