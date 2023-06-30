using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    private void OnDestroy()
    {
        //Remove position from crowd density array 
        GenerateAgentAttributes GA = gameObject.GetComponent<GenerateAgentAttributes>();
        CrowdDensity scriptA = FindObjectOfType<CrowdDensity>();
        scriptA.ValidPoints[GA.HeatMapArrayPos] = 0f;
    }
}
