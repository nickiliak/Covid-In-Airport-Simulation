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
        AgentData agentdata = gameObject.GetComponent<AgentData>();
        Node root = new Selector(new List<Node>
            {
                new Boarding(navMeshAgent, EntryGateNumber, gameObject),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("BathroomBool1", agentSettings.ChanceToUseRestroom),
                    new BotMiddleBathroom(navMeshAgent, "BathroomBool1", agentdata)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("CheckInBool", agentSettings.ChanceToCheckIn),
                    new CheckIn(navMeshAgent,"CheckInBool")
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("BathroomBool2", agentSettings.ChanceToUseRestroom),
                    new TopMiddleBathroom(navMeshAgent, "BathroomBool2", agentdata)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("ShopBool", agentSettings.ChanceToShop),
                    new Shop(navMeshAgent, "ShopBool", agentdata)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfWeActivateBehavior("EatBool", agentSettings.ChanceToEat),
                    new Eat(navMeshAgent, "EatBool", agentdata)
                }),
                new WaitingUntilBoard(navMeshAgent, agentdata)
            });

        return root;
    }
}
