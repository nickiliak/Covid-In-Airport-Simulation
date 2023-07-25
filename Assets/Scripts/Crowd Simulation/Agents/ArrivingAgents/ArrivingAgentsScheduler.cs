using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivingAgentsScheduler : MonoBehaviour
{
    float TimeUntilNextBatch;
    // Start is called before the first frame update
    void Start()
    {
        TimeUntilNextBatch = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - TimeUntilNextBatch > 15f ) 
        {
            TimeUntilNextBatch = Time.time;
            FindObjectOfType<ArrivingAgentsSpawner>().SpawnAgents();
        }
    }
}
