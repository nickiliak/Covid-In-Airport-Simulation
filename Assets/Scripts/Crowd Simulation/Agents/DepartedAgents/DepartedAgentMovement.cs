using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Generating and executing the path the agent will use based on parameters
/// </summary>
public class DepartedAgentMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public DepartedAgentsPathGenerator.AgentState agentState;
    public DepartedAgentSpawner.DepartedAgentSettings agentSettings;

    private DepartedAgentsPathGenerator AgentPath;
    private bool NeedsRestroom = false;
    private bool NeedsBaggage = false;
    private bool NeedsCar = false;

    private float startTimeOfWaiting;
    private float CurrentWaitingTime = 0f;

    List<Vector3> destinations;
    List<float> waitTimes;
    
    void Start()
    {
        //Initial State
        agentState = DepartedAgentsPathGenerator.AgentState.None;

        //Randomly make them need to use the bathroom or not
        if (Random.value < agentSettings.ChanceToUseRestroom) NeedsRestroom = true;
        if (Random.value < agentSettings.ChanceToHaveBaggage) NeedsBaggage = true;
        if (Random.value < agentSettings.ChanceToWantACar) NeedsCar = true;
        
        GameObject myTextObject = GameObject.Find("AgentCounterText");
        AgentTextCounter myText =  myTextObject.GetComponent<AgentTextCounter>();
        myText.IncreaseAgentCounter();

        //Generate the path the agents will walk on based on the parameters
        AgentPath = new DepartedAgentsPathGenerator(name, NeedsRestroom, NeedsBaggage, NeedsCar);
        destinations = AgentPath.Destinations[0];
        waitTimes = AgentPath.WaitTimes[0];
        agentState = AgentPath.States[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(IsWaitingOver()) PathExecution();
    }
    void PathExecution()
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
                    //Special Cases
                    if (agentState == DepartedAgentsPathGenerator.AgentState.BaggageClaim && destinations.Count == 1) Destroy(AgentPath.SuitCase);
                    if (agentState == DepartedAgentsPathGenerator.AgentState.ExitAirport) { Destroy(gameObject); return; } 

                    navMeshAgent.ResetPath();
                    destinations.Remove(destinations[0]);
                    startTimeOfWaiting = Time.time;
                    CurrentWaitingTime = waitTimes[0];
                    waitTimes.Remove(waitTimes[0]);
                }
            }
        }
        else if (destinations.Count == 0)
        {
            if (AgentPath.Destinations.Count != 0)
            {
                AgentPath.Destinations.Remove(AgentPath.Destinations[0]);
                AgentPath.WaitTimes.Remove(AgentPath.WaitTimes[0]);
                AgentPath.States.Remove(AgentPath.States[0]);
            }

            if (AgentPath.Destinations.Count != 0)
            {
                destinations = AgentPath.Destinations[0];
                waitTimes = AgentPath.WaitTimes[0];
                agentState = AgentPath.States[0];
            }
        }
    }

    bool IsWaitingOver()
    {
        return (Time.time - startTimeOfWaiting >= CurrentWaitingTime);
    }
}


