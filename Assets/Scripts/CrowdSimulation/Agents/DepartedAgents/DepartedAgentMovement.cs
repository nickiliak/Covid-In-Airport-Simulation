using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DepartedAgentMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public DepartedAgentsPathGenerator.AgentState agentState;
    public DepartedAgentSpawner.DepartedAgentSettings agentSettings;

    private DepartedAgentsPathGenerator AgentPath;
    private bool NeedsRestroom = false;
    private bool NeedsBaggage = false;
    private bool NeedsCar = false;

    List<Vector3> destinations;
    List<float> waitTimes;
    DepStatsData DepStats;

    SpawnAgentBaggage SpawnBaggage;
    GameObject SuitCase;
    

    void Start()
    {
        //Initial State
        agentState = DepartedAgentsPathGenerator.AgentState.None;

        //Randomly make them need to use the bathroom or not
        if (Random.value < agentSettings.ChanceToUseRestroom) NeedsRestroom = true;
        if (Random.value < agentSettings.ChanceToHaveBaggage)
        {
            //Pick one of two possible conveyor belts
            if (Random.Range(0, 2) == 0) SpawnBaggage = GameObject.Find("BaggageSpawner1").GetComponent<SpawnAgentBaggage>();
            else SpawnBaggage = GameObject.Find("BaggageSpawner2").GetComponent<SpawnAgentBaggage>();

            //Spawn SuitCase
            SuitCase = SpawnBaggage.SpawnBaggage(name);

            NeedsBaggage = true;
        }
        if (Random.value < agentSettings.ChanceToWantACar) NeedsCar = true;

        AgentPath = new DepartedAgentsPathGenerator(NeedsRestroom, NeedsBaggage, NeedsCar);
        destinations = AgentPath.Destinations[0];
        waitTimes = AgentPath.WaitTimes[0];
        agentState = AgentPath.States[0];
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StateBehavior());
    }
    IEnumerator StateBehavior()
    {

        if (destinations.Count != 0)
        {
            if (navMeshAgent.hasPath == false)
            {
                navMeshAgent.destination = destinations[0];
            }
            else
            {
                if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2.5f)
                {
                    navMeshAgent.ResetPath();
                    destinations.Remove(destinations[0]);
                    yield return new WaitForSeconds(waitTimes[0]);
                    waitTimes.Remove(waitTimes[0]);
                }
            }
        }
        else if (destinations.Count == 0)
        {
            AgentPath.Destinations.Remove(AgentPath.Destinations[0]);
            AgentPath.WaitTimes.Remove(AgentPath.WaitTimes[0]);
            AgentPath.States.Remove(AgentPath.States[0]);

            destinations = AgentPath.Destinations[0];
            waitTimes = AgentPath.WaitTimes[0];
            agentState = AgentPath.States[0];
        }


        /*if (agentState == AgentState.BaggageClaim) //Special Case
        {
            if (Vector3.Distance(transform.position, SuitCase.transform.position) < 2.5f)
            {
                agentState = AgentState.None;
                Destroy(SuitCase);
            }
        }*/
    }
}


