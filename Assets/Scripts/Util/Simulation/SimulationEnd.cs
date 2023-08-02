using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SimulationEnd : MonoBehaviour
{
    SimulationData sd;

    string ParametersFilename = "";
    string DataFilename = "";

    public void GenerateData()
    {
        TextWriter parameters = new StreamWriter(ParametersFilename, false);
        parameters.WriteLine("Simulation has been executed.");
        parameters.WriteLine("Total Time elapsed:" + sd.EndingTime);
        parameters.WriteLine("");
        parameters.WriteLine("Parameters Used:");
        parameters.WriteLine("Virus: [" 
            + "Virus Infectiousness: " + sd.virusData.GetVirusInfectiousness().ToString() + "," 
            + " InfectionRange: " + sd.virusData.GetInfectionRange().ToString() + ","
            + " TotalNumberOfInfected: " + sd.virusData.GetTotalNumberOfInfected().ToString() + "]");
        parameters.Close();

        TextWriter data = new StreamWriter(DataFilename, false);
        data.WriteLine("Susceptible, Infected, Exposed, Time");
        for (int i = 0; i < sd.RData.Count; i++)
        {
            data.WriteLine(sd.RData[i].GetCurrentNumberOfSuscpetible() + "," +
                sd.RData[i].GetCurrentNumberOfInfected() + "," +
                sd.RData[i].GetTotalNumberOfExposed() + "," +
                sd.RData[i].GetTime());
        }
        data.Close();
    }


    private void OnEnable()
    {
        ParametersFilename = Application.dataPath + "/GeneratedData/AirportSimulation_Parameters.txt";
        DataFilename = Application.dataPath + "/GeneratedData/Data.csv";
        sd = FindAnyObjectByType<SimulationData>();
        GenerateData();
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
