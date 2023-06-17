using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrivingAgentsSpawner : MonoBehaviour
{
    public GameObject agentPrefab; // Prefab of the agent to spawn
    public GameObject Parent;
    public float minSpawnDelay = 10f; // Minimum delay between spawns
    public float maxSpawnDelay = 30f; // Maximum delay between spawns
    public int minSpawnCount = 10;
    public int maxSpawnCount = 20;
    public float waitTime = 120f;

    private int FlightNo = 0;
    private List<GameObject> agents = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAFlight());
    }
    private IEnumerator StartAFlight()
    {
        
        while (true)
        {
            if(agents.Count != 0 )
            {
                foreach (GameObject agent in agents)
                {
                    ArrivingAgentsMovement agentScript = agent.GetComponent<ArrivingAgentsMovement>();
                    agentScript.TimeToBoard = true;
                }

                agents.Clear();
            }

            int agentCount = Random.Range(minSpawnCount, maxSpawnCount);
            Color randomColor = Random.ColorHSV();
            FlightNo++;

            int agentGate = Random.Range(1, 4); 

            for (int i = 0; i < agentCount; i++)
            {
                Vector3 AgentPos = transform.position;
                AgentPos.x = AgentPos.x + Random.Range(-80f, 80f);
                GameObject newAgent = Instantiate(agentPrefab, AgentPos, Quaternion.identity);
                agents.Add(newAgent);

                //Set Parent for agents so that its organized
                newAgent.transform.parent = Parent.transform;

                //Agent Number and Flight Number
                newAgent.name = "FlightNo" + FlightNo.ToString() + "_AgentNo" + i.ToString();

                //Set color for this agent so all agents of this flight have the same color
                Renderer agentRenderer = newAgent.GetComponent<Renderer>();
                agentRenderer.material.color = randomColor;

                ArrivingAgentsMovement agentScript = newAgent.GetComponent<ArrivingAgentsMovement>();
                agentScript.EntryGateNumber = agentGate;

                //Wait a little bit until next Agent
                float spawnDelay = Random.Range(0.1f, 2f);
                yield return new WaitForSeconds(spawnDelay);
            }

            //Wait until next flight is ready
            float randomSpawnInterval = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(randomSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
