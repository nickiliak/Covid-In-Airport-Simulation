using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusData
{
    private int CurrentNumberOfSusceptible = 0;
    private int CurrentNumberOfInfected = 0;
    private int CurrentNumberOfExposed = 0;

    private int TotalNumberOfSusceptible = 0;
    private int TotalNumberOfInfected = 0;
    private int TotalNumberOfExposed = 0;

    private float VirusInfectiousness = 0;
    private float InfectionRange = 0;
    private bool MaskWearing = true;
    private bool SocialDistancing = true;

    public void UpdateVirusInfectiousness(float newInfectiousness) { VirusInfectiousness = newInfectiousness; }
    public void UpdateInfectionRange(float newInfectionRange) { InfectionRange = newInfectionRange; }
    public void UpdateMaskWearing(bool newMaskWearing) { MaskWearing = newMaskWearing; }
    public void UpdateSocialDistancing(bool newSocialDistancing) { SocialDistancing =  newSocialDistancing; }

    public float GetVirusInfectiousness() { return VirusInfectiousness; }
    public float GetInfectionRange() { return InfectionRange; }
    public bool GetMaskWearing() { return MaskWearing; }
    public bool GetSocialDistancing() {  return SocialDistancing; }

    /// <summary>
    /// CurrentNumbers Functions
    /// </summary>
    public void IncreaseCurrentNumberOfInfected() { CurrentNumberOfInfected++; }
    public void IncreaseCurrentNumberOfExposed() { CurrentNumberOfExposed++; }
    public void IncreaseCurrentNumberOfSusceptible() { CurrentNumberOfSusceptible++; }

    public void DecreaseCurrentNumberOfInfected() { CurrentNumberOfInfected--; }
    public void DecreaseCurrentNumberOfExposed() { CurrentNumberOfExposed--; }
    public void DecreaseCurrentNumberOfSusceptible() { CurrentNumberOfSusceptible--; }

    public int GetCurrentNumberOfSusceptible() { return CurrentNumberOfSusceptible; }
    public int GetCurrentNumberOfInfected() { return CurrentNumberOfInfected; }
    public int GetCurrentNumberOfExposed() { return CurrentNumberOfExposed; }


    /// <summary>
    /// TotalNumbers Functions
    /// </summary>
    // Increasers (default +1) Totals
    public void IncreaseTotalNumberOfSusceptible() { TotalNumberOfSusceptible++; }
    public void IncreaseTotalNumberOfInfected() { TotalNumberOfInfected++; }
    public void IncreaseTotalNumberOfExposed() { TotalNumberOfExposed++; }

    // Decreasers (default -1) Totals
    public void DecreaseTotalNumberOfSusceptible() { TotalNumberOfSusceptible--; }
    public void DecreaseTotalNumberOfInfected() { TotalNumberOfInfected--; }
    public void DecreaseTotalNumberOfExposed() { TotalNumberOfExposed--; }

    // Setters for private fields Totals
    public void SetTotalNumberOfSusceptible(int value) { TotalNumberOfSusceptible = value; }
    public void SetTotalNumberOfInfected(int value) { TotalNumberOfInfected = value; }
    public void SetTotalNumberOfExposed(int value) { TotalNumberOfExposed = value; }

    // Getters for private fields Totals
    public int GetTotalNumberOfSusceptible() { return TotalNumberOfSusceptible; }
    public int GetTotalNumberOfInfected() { return TotalNumberOfInfected; }
    public int GetTotalNumberOfExposed() { return TotalNumberOfExposed; }
}
