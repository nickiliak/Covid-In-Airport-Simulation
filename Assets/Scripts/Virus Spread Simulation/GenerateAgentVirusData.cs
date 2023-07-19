using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateAgentVirusData
{
    const int SUSCEPTIBLE = 0;
    const int INFECTED = 2;

    Color SUSCEPTIBLECOLOR = Color.blue;
    Color EXPOSEDCOLOR = Color.yellow;
    Color INFECTEDCOLOR = Color.red;
    Color RECOVEREDCOLOR = Color.green;

    // IMPORTANT WE DONT GENERATE RECORVERED AGENTS FOR THE TIME BEING OR EXPOSED! SO ONLY S I
    public int GenerateViralStateValue(SimulationData sd)
    { 
        if (sd.NumberOfInfected >= sd.MaximumNumberOfInfected) 
        {
            return SUSCEPTIBLE;
        }
        else
        {
            if (Random.Range(0, 2) == 0) return SUSCEPTIBLE;
            else return INFECTED;
        }

    }   

    public Color GenerateViralStateColor(int ViralStateValue) {
        if (ViralStateValue == 0) return SUSCEPTIBLECOLOR;
        else if (ViralStateValue == 1) return EXPOSEDCOLOR;
        else if (ViralStateValue == 2) return INFECTEDCOLOR;
        else return RECOVEREDCOLOR;
    }

}
