using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DepartedAgentsBT : BehaviorTree.Tree
{
    public NavMeshAgent navMeshAgent;
    public enum AgentAction { None, Restroom, BaggageClaim, CarRental, ExitAirport }
    public AgentAction Action;

    public DepartedAgentSpawner.DepartedAgentSettings agentSettings;


    protected override Node SetupTree()
    {
        AgentData agentdata = gameObject.GetComponent<AgentData>();
        Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("BathroomBool", agentSettings.ChanceToUseRestroom),
                    new TopLeftBathroom(navMeshAgent, "BathroomBool", agentdata)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("RandomizePath", 1f),
                    new RandomizePath(navMeshAgent, "RandomizePath")
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("BaggageBool", agentSettings.ChanceToHaveBaggage),
                    new Baggage(navMeshAgent, "BaggageBool", gameObject)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("CarBool", agentSettings.ChanceToWantACar),
                    new Car(navMeshAgent, "CarBool")
                }),
                new Exit(navMeshAgent, gameObject)
            });

        return root;
    }
}
