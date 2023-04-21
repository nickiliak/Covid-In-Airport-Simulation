using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArrivingAgentsMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public enum AgentState { None, CheckIn, Restroom, Shop, Eat, Board }
    public AgentState agentState;

    public float ChanceToUseRestroom = 0.33f;
    public float ChanceToShop= 0.8f;
    public float ChanceToEat = 0.2f;

    private bool NeedsRestroom = false;
    private bool NeedsShop = false;
    private bool NeedsEat = false;

    // Start is called before the first frame update
    void Start()
    {
        //Initial State
        agentState = AgentState.None;

        //Randomly make them need to use the bathroom or not
        if (Random.value < ChanceToUseRestroom) NeedsRestroom = true;
        if (Random.value < ChanceToShop) NeedsShop = true;
        if (Random.value < ChanceToEat) NeedsEat = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
