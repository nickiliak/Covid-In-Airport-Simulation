using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArrivingAgentsBT : BehaviorTree.Tree
{
    public NavMeshAgent navMeshAgent;
    public enum AgentAction { None, CheckIn, Restroom, Shop, Eat, WaitingUntilBoard, Board }
    public AgentAction Action;

    public ArrivingAgentsSpawner.ArrivingAgentSettings agentSettings;
    public int EntryGateNumber;
    public bool TimeToBoard = false;
    

    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
            {
                new Boarding(navMeshAgent, EntryGateNumber, gameObject),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("BathroomBool1", agentSettings.ChanceToUseRestroom),
                    new Bathroom_1(navMeshAgent, "BathroomBool1")
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("CheckInBool", agentSettings.ChanceToCheckIn),
                    new CheckIn(navMeshAgent,"CheckInBool")
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("BathroomBool2", agentSettings.ChanceToUseRestroom),
                    new Bathroom_2(navMeshAgent, "BathroomBool2")
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("ShopBool", agentSettings.ChanceToShop),
                    new Shop(navMeshAgent, "ShopBool")
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("EatBool", agentSettings.ChanceToEat),
                    new Eat(navMeshAgent, "EatBool")
                }),
                new WaitingUntilBoard(navMeshAgent)
            });

        return root;
    }
}
