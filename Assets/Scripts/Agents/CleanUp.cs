using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    private void OnDestroy()
    {
        //Remove position from crowd density array 
        GenerateAgentAttributes GA = gameObject.GetComponent<GenerateAgentAttributes>();
        CrowdDensity CD = FindObjectOfType<CrowdDensity>();
        if(CD != null) CD.ValidPoints[GA.CrowdDensityPos] = 0f;
    }
}
