using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrivingAgentsSpawner : MonoBehaviour
{

    public GameObject agentPrefab; // Prefab of the agent to spawn
    public GameObject Parent;
    int agentGate;

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

    private GameObject CrowdDensity;
    SimulationData sData;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Visualizations = GameObject.Find("Visualizations");
        CrowdDensity = Visualizations.transform.Find("CrowdDensity").gameObject;
        sData = FindObjectOfType<SimulationData>(); 
    }

    public void SpawnAgents(int AgentNumber, float BoardingTime, int FlightNumber)
    {
        StartCoroutine(AgentsArriving(AgentNumber, BoardingTime, FlightNumber));
    }

    void SpawnAgent(List<GameObject> agents, int FlightNumber, int i, Vector3 AgentPos)
    {
        GameObject newAgent = Instantiate(agentPrefab, AgentPos, Quaternion.identity);
        agents.Add(newAgent);

        //Set Parent for agents so that its organized
        newAgent.transform.parent = Parent.transform;

        //Agent Number and Flight Number
        newAgent.name = "FlightNo" + FlightNumber.ToString() + "_AgentNo" + i.ToString();

        //If crowd shader is active we need to detect collisions not triggers
        if (CrowdDensity.activeInHierarchy) newAgent.GetComponent<CapsuleCollider>().isTrigger = false;


        //Set color for this agent so all agents of this flight have the same color
        //Renderer agentRenderer = newAgent.GetComponent<Renderer>();
        //agentRenderer.material.color = randomColor;

        //Pass the settings to the agent
        ArrivingAgentsBT agentScript = newAgent.GetComponent<ArrivingAgentsBT>();
        agentScript.EntryGateNumber = agentGate;
        agentScript.agentSettings = AgentSettings;

        //Add agent to simulation data for general use
        sData.GetAirportData().InsertNewIncomingAgent(newAgent);
    }

    private IEnumerator AgentsArriving(int AgentNumber, float BoardingTime, int FlightNumber)
    {
        //Color randomColor = Random.ColorHSV();
        List<GameObject> agents = new List<GameObject>();
        agentGate = Random.Range(1, 4);
        int i = 0;
        while (i < AgentNumber)
        {
            Vector3 AgentPos = transform.position;
            AgentPos.x = AgentPos.x + Random.Range(-100f, 100f);
            SpawnAgent(agents, FlightNumber, i++, AgentPos);

            //Wait a little bit until next Agent
            float spawnDelay = Random.Range(0f, 1f);
            yield return new WaitForSeconds(spawnDelay);
        }

        float delay = BoardingTime - Time.time;
        if (delay > 0) yield return new WaitForSeconds(delay); //Wait X time until time to board has arrived meanin delay function until the time has come

        if (agents.Count != 0)
        {
            foreach (GameObject agent in agents)
            {
                ArrivingAgentsBT agentScript = agent.GetComponent<ArrivingAgentsBT>();
                agentScript.TimeToBoard = true;
            }

            agents.Clear();
        }
    }
}
