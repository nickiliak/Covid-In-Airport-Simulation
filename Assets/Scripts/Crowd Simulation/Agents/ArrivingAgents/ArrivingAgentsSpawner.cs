using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrivingAgentsSpawner : MonoBehaviour
{

    [Serializable]
    public class ArrivingAgentSettings
    {
        [Range(0f, 1f)]
        public float ChanceToUseRestroom;
        [Range(0f, 1f)]
        public float ChanceToCheckIn;
        [Range(0f, 1f)]
        public float ChanceToShop;
        [Range(0f, 1f)]
        public float ChanceToEat;
    }
    public ArrivingAgentSettings AgentSettings = new();

    [Serializable]
    public class ArrivingAgentSpawnerSettings
    {
        public GameObject agentPrefab; // Prefab of the agent to spawn
        public GameObject Parent;
        public int minSpawnCount;
        public int maxSpawnCount;
        public float waitTime;
    }
    public ArrivingAgentSpawnerSettings ArrivingSpawnerSettings = new();

    private GameObject CrowdDensity;
    private int FlightNo = 0;
    private List<GameObject> agents = new List<GameObject>();
    SimulationData sData;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Visualizations = GameObject.Find("Visualizations");
        CrowdDensity = Visualizations.transform.Find("CrowdDensity").gameObject;
        sData = FindObjectOfType<SimulationData>(); 
    }

    public void SpawnAgents()
    {
        StartCoroutine(AgentsArriving());
    }

    private IEnumerator AgentsArriving()
    {
        int agentCount = Random.Range(ArrivingSpawnerSettings.minSpawnCount, ArrivingSpawnerSettings.maxSpawnCount);
        Color randomColor = Random.ColorHSV();
        FlightNo++;

        int agentGate = Random.Range(1, 4); 

        for (int i = 0; i < agentCount; i++)
        {
            Vector3 AgentPos = transform.position;
            AgentPos.x = AgentPos.x + Random.Range(-40f, 40f);
            GameObject newAgent = Instantiate(ArrivingSpawnerSettings.agentPrefab, AgentPos, Quaternion.identity);
            agents.Add(newAgent);

            //Set Parent for agents so that its organized
            newAgent.transform.parent = ArrivingSpawnerSettings.Parent.transform;

            //Agent Number and Flight Number
            newAgent.name = "FlightNo" + FlightNo.ToString() + "_AgentNo" + i.ToString();

            //If crowd shader is active we need to detect collisions not triggers
            if(CrowdDensity.activeInHierarchy) newAgent.GetComponent<CapsuleCollider>().isTrigger = false;


            //Set color for this agent so all agents of this flight have the same color
            Renderer agentRenderer = newAgent.GetComponent<Renderer>();
            agentRenderer.material.color = randomColor;

            //Pass the settings to the agent
            ArrivingAgentsMovement agentScript = newAgent.GetComponent<ArrivingAgentsMovement>();
            agentScript.EntryGateNumber = agentGate;
            agentScript.agentSettings = AgentSettings;

            //Add agent to simulation data for general use
            sData.InsertNewIncomingAgent(newAgent);

            //Wait a little bit until next Agent
            float spawnDelay = Random.Range(0.1f, 1f);
            yield return new WaitForSeconds(spawnDelay);
        }

        yield return new WaitForSeconds(ArrivingSpawnerSettings.waitTime); //Wait X time until time to board has arrived

        if (agents.Count != 0)
        {
            foreach (GameObject agent in agents)
            {
                ArrivingAgentsMovement agentScript = agent.GetComponent<ArrivingAgentsMovement>();
                agentScript.TimeToBoard = true;
            }

            agents.Clear();
        }
    }
}
