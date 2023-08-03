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
    string DataFilename = "";

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

        TextWriter data = new StreamWriter(DataFilename, false);
        data.WriteLine("Susceptible,Infected,Exposed,Time");
        for (int i = 0; i < sd.GetRecordedData().Count; i++)
        {
            data.WriteLine(sd.GetRecordedData()[i].GetCurrentNumberOfSuscpetible() + "," +
                sd.GetRecordedData()[i].GetCurrentNumberOfInfected() + "," +
                sd.GetRecordedData()[i].GetTotalNumberOfExposed() + "," +
                sd.GetRecordedData()[i].GetTime());
        }
        data.Close();
    }


    private void OnEnable()
    {
        SR.SetActive(false);
        sd = FindAnyObjectByType<SimulationData>();
        ParametersFilename = Application.dataPath + "/DataGeneration/AirportSimulation_Parameters.txt";
        DataFilename = Application.dataPath + "/DataGeneration/Datasets/dataset" + sd.currentRepeat.ToString() + ".csv";

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
