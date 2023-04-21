using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivingAgentsSpawner : MonoBehaviour
{
    public GameObject agentPrefab; // Prefab of the agent to spawn
    public float minSpawnDelay = 1f; // Minimum delay between spawns
    public float maxSpawnDelay = 5f; // Maximum delay between spawns

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnAgent", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void SpawnAgent()
    {
        // Instantiate a new instance of the agent prefab
        Vector3 AgentPos = transform.position;
        AgentPos.x = AgentPos.x + Random.Range(-80f, 80f);
        GameObject newAgent = Instantiate(agentPrefab, AgentPos, Quaternion.identity);

        // Set any necessary properties on the agent
        // ...

        // Add any necessary components to the new agent GameObject
        // ...

        // Schedule the next agent spawn at a random interval
        Invoke("SpawnAgent", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
