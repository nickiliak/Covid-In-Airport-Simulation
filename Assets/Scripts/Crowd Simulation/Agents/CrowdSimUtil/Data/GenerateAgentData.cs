using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

/// <summary>
/// Generating the basic data for an agent
/// </summary>
public class GenerateAgentData
{
    // Start is called before the first frame update

    public int GenerateAge() { return UnityEngine.Random.Range(10, 90); }
    public int GenerateGenderValue() { return UnityEngine.Random.Range(0, 2); }
    public float GenerateSpeed() { return UnityEngine.Random.Range(2f, 4f); }
    public float GenerateAccelaration() { return UnityEngine.Random.Range(4f, 10f); }
    public float GenerateProximity() { return UnityEngine.Random.Range(0.2f, 0.5f); } 
    public float GenerateDrag() { return 100f; }
    public float GenerateAngularDrag() { return 100f; }

}
