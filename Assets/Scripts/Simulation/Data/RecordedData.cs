using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedData
{
    private int CurrentNumberOfSuscpetible = 0;
    private int CurrentNumberOfInfected = 0;
    private int TotalNumberOfExposed = 0;
    private float Time = 0f;

    private string Area;
    private int Hit;

    public RecordedData(int currentNumberOfSusceptible, int currentNumberOfInfected, int totalNumberOfExposed, float time)
    {
        CurrentNumberOfSuscpetible = currentNumberOfSusceptible;
        CurrentNumberOfInfected = currentNumberOfInfected;
        TotalNumberOfExposed = totalNumberOfExposed;
        Time = time;
    }

    public RecordedData(string area, int hit) 
    {
        Area = area;
        Hit = hit;
    }

    // Getters for private fields
    public int GetCurrentNumberOfSuscpetible() { return CurrentNumberOfSuscpetible; }
    public int GetCurrentNumberOfInfected() { return CurrentNumberOfInfected; }
    public int GetTotalNumberOfExposed() { return TotalNumberOfExposed; }
    public float GetTime() { return Time; }


    public string GetArea() { return Area; }    
    public int GetHit() { return Hit; }

}
