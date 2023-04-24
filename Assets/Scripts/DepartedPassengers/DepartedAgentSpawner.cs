using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartedAgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab; // Prefab of the agent to spawn
    public float minSpawnDelay = 10f; // Minimum delay between spawns
    public float maxSpawnDelay = 30f; // Maximum delay between spawns
    public int minSpawnCount = 10;
    public int maxSpawnCount = 20;
    public float waitTime = 30f;

    private int FlightNo = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaneHasArrived());
    }

    private IEnumerator PlaneHasArrived()
    {
        while (true)
        {
            int agentCount = Random.Range(minSpawnCount, maxSpawnCount);
            Color randomColor = Random.ColorHSV();
            FlightNo++;

            for (int i = 0; i < agentCount; i++)
            {
                //Spawn Agent
                Vector3 AgentPos = transform.position;
                AgentPos.x = AgentPos.x + Random.Range(-5f, 5f);
                GameObject newAgent = Instantiate(agentPrefab, AgentPos, Quaternion.identity);

                //Agent Flight Number and Number
                newAgent.name = "FlightNo" + FlightNo.ToString() + "_AgentNo" + i.ToString();

                //Set color for this agent so all agents of this flight have the same color
                Renderer agentRenderer = newAgent.GetComponent<Renderer>();
                agentRenderer.material.color = randomColor;

                //Wait a little bit until next Agent
                float spawnDelay = Random.Range(0.1f, 2f);
                yield return new WaitForSeconds(spawnDelay);
            }

            //Wait until next batch of Agents comes
            float randomSpawnInterval = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(randomSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
