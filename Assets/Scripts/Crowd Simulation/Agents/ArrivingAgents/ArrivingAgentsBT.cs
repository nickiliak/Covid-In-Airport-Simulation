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
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("BathroomBool1", agentSettings.ChanceToUseRestroom),
                    new Bathroom_1(navMeshAgent, "BathroomBool1", out Action)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("CheckInBool", agentSettings.ChanceToCheckIn),
                    new CheckIn(navMeshAgent,"CheckInBool", out Action)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("BathroomBool2", agentSettings.ChanceToUseRestroom),
                    new Bathroom_2(navMeshAgent, "BathroomBool2", out Action)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("ShopBool", agentSettings.ChanceToShop),
                    new Shop(navMeshAgent, "ShopBool", out Action)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("EatBool", agentSettings.ChanceToEat),
                    new Eat(navMeshAgent, "EatBool", out Action)
                }),
                new Boarding(navMeshAgent, out Action, EntryGateNumber, gameObject),
                new WaitingUntilBoard(navMeshAgent, out Action)
            });

        return root;
    }
}
