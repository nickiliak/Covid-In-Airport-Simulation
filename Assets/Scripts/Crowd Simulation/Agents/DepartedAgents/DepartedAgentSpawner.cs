using System;
using System.Collections;
using UnityEngine;

public class DepartedAgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab; // Prefab of the agent to spawn
    public GameObject Parent;      // Parent to spawn the object below to

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

    private GameObject CrowdDensity;
    SimulationData sData;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Visualizations = GameObject.Find("Visualizations");
        CrowdDensity = Visualizations.transform.Find("CrowdDensity").gameObject;
        sData = FindObjectOfType<SimulationData>();
    }

    public void SpawnAgents(int AgentNumber, int FlightNumber)
    {
        StartCoroutine(PlaneWithAgentsArrived(AgentNumber, FlightNumber));
    }

    private IEnumerator PlaneWithAgentsArrived(int AgentNumber, int FlightNumber)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            //Spawn Agent
            Vector3 AgentPos = transform.position;
            AgentPos.x = AgentPos.x + UnityEngine.Random.Range(-5f, 5f);
            GameObject newAgent = Instantiate(agentPrefab, AgentPos, Quaternion.identity);

            //Set Agents settings
            newAgent.GetComponent<DepartedAgentMovement>().agentSettings = AgentSettings;

            //If crowd shader is active we need to detect collisions not triggers
            if (CrowdDensity.activeInHierarchy) newAgent.GetComponent<CapsuleCollider>().isTrigger = false;

            //Set Parent for agents so that its organized
            if (Parent != null) newAgent.transform.parent = Parent.transform;

            //Agent Flight Number and Number
            newAgent.name = "FlightNo" + FlightNumber.ToString() + "_AgentNo" + i.ToString();

            //Add agent to simulation data for general use
            sData.InsertNewOutGoingAgent(newAgent);

            //Wait a little bit until next Agent
            float spawnDelay = UnityEngine.Random.Range(0.5f, 1f);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
