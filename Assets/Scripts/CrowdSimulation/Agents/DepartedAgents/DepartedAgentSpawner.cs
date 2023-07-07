using System;
using System.Collections;
using UnityEngine;

public class DepartedAgentSpawner : MonoBehaviour
{

    [Serializable]
    public class DepartedAgentSettings
    {
        [Range(0f, 1f)]
        public float ChanceToUseRestroom = 0.33f;
        [Range(0f, 1f)]
        public float ChanceToHaveBaggage = 0.8f;
        [Range(0f, 1f)]
        public float ChanceToWantACar = 0.2f;
    }
    public DepartedAgentSettings AgentSettings = new();


    [Serializable]
    public class DepartedAgentSpawnerSettings
    {
        public GameObject agentPrefab; // Prefab of the agent to spawn
        public GameObject Parent;      // Parent to spawn the object below to
        public float minSpawnDelay = 10f; // Minimum delay between spawns
        public float maxSpawnDelay = 30f; // Maximum delay between spawns
        public int minSpawnCount = 10;
        public int maxSpawnCount = 20;
        [Range(0f, 120f)]
        public float waitTime = 30f;
    }
    public DepartedAgentSpawnerSettings DepartedSpawnerSettings = new();


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
            int agentCount = UnityEngine.Random.Range(DepartedSpawnerSettings.minSpawnCount, DepartedSpawnerSettings.maxSpawnCount);
            Color randomColor = UnityEngine.Random.ColorHSV();
            FlightNo++;

            for (int i = 0; i < agentCount; i++)
            {
                //Spawn Agent
                Vector3 AgentPos = transform.position;
                AgentPos.x = AgentPos.x + UnityEngine.Random.Range(-5f, 5f);
                GameObject newAgent = Instantiate(DepartedSpawnerSettings.agentPrefab, AgentPos, Quaternion.identity);

                //Set Agents settings
                newAgent.GetComponent<DepartedAgentMovement>().agentSettings = AgentSettings;

                //Set Parent for agents so that its organized
                if(DepartedSpawnerSettings.Parent != null) newAgent.transform.parent = DepartedSpawnerSettings.Parent.transform;

                //Agent Flight Number and Number
                newAgent.name = "FlightNo" + FlightNo.ToString() + "_AgentNo" + i.ToString();

                //Set color for this agent so all agents of this flight have the same color
                Renderer agentRenderer = newAgent.GetComponent<Renderer>();
                agentRenderer.material.color = randomColor;

                //Wait a little bit until next Agent
                float spawnDelay = UnityEngine.Random.Range(0.1f, 2f);
                yield return new WaitForSeconds(spawnDelay);
            }

            //Wait until next batch of Agents comes
            float randomSpawnInterval = UnityEngine.Random.Range(DepartedSpawnerSettings.minSpawnDelay, DepartedSpawnerSettings.maxSpawnDelay);
            yield return new WaitForSeconds(randomSpawnInterval);
            yield return new WaitForSeconds(DepartedSpawnerSettings.waitTime);
        }
    }
}
