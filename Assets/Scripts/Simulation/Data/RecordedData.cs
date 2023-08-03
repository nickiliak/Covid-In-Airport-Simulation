using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedData
{
    private int CurrentNumberOfSuscpetible = 0;
    private int CurrentNumberOfInfected = 0;
    private int TotalNumberOfExposed = 0;
    private float Time = 0f;

    public RecordedData(int currentNumberOfSusceptible, int currentNumberOfInfected, int totalNumberOfExposed, float time)
    {
        CurrentNumberOfSuscpetible = currentNumberOfSusceptible;
        CurrentNumberOfInfected = currentNumberOfInfected;
        TotalNumberOfExposed = totalNumberOfExposed;
        Time = time;
    }

    // Getters for private fields
    public int GetCurrentNumberOfSuscpetible() { return CurrentNumberOfSuscpetible; }
    public int GetCurrentNumberOfInfected() { return CurrentNumberOfInfected; }
    public int GetTotalNumberOfExposed() { return TotalNumberOfExposed; }
    public float GetTime() { return Time; }
}
