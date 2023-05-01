using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class DepartedAgentMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public enum AgentState {None, Restroom, BaggageClaim, CarRental, ExitAirport }
    public AgentState agentState;

    private float ChanceToUseRestroom = 0.33f;
    private float ChanceToHaveBaggage = 0.8f;
    private float ChanceToWantACar = 0.2f;

    private bool NeedsRestroom = false;
    private bool NeedsBaggage = false;
    private bool NeedsCar = false;

    private bool AreaBehaviour = false;
    private bool Waiting = false;
    private List<Vector3> destinations = new List<Vector3>();
    private List<float> WaitTime = new List<float>();

    private GameObject Airport;
    private string Buildings = "Second Section/Buildings";
    private string Areas = "Second Section/Areas";

    void Start()
    {
        //Initial State
        agentState = AgentState.None;
        Airport = GameObject.Find("Airport");

        //Randomly make them need to use the bathroom or not
        if (Random.value < ChanceToUseRestroom) NeedsRestroom = true;
        if (Random.value < ChanceToHaveBaggage) NeedsBaggage = true;
        if (Random.value < ChanceToWantACar) NeedsCar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agentState == AgentState.None && AreaBehaviour == false) ControlState();
        if (AreaBehaviour == false) ActivateStateBehavior();
        if (AreaBehaviour == true && Waiting == false) StartCoroutine(StateBehavior());
    }

    void ControlState() {
        if (NeedsRestroom)
        {
            agentState = AgentState.Restroom;
            navMeshAgent.destination = Airport.transform.Find(Buildings + "/Bathroom (1)").position;
            NeedsRestroom = false;
        }
        else if (NeedsBaggage)
        {
            agentState = AgentState.BaggageClaim;
            navMeshAgent.destination = new Vector3(73, 0, 18);
            NeedsBaggage = false;
        }
        else if (NeedsCar)
        {
            agentState = AgentState.CarRental;
            navMeshAgent.destination = new Vector3(96, 0, 36);
            NeedsCar = false;
        }
        else
        {
            agentState = AgentState.ExitAirport;
            navMeshAgent.destination = new Vector3(129, 0, -24);
        }
    }
    void ActivateStateBehavior()
    {
        if(Vector3.Distance(transform.position, navMeshAgent.destination) < 2f)
        {
            switch (agentState)
            {
                case AgentState.Restroom:
                    //Randomly select Toilet and sink
                    destinations.Add(Airport.transform.Find(Buildings + "/Bathroom (1)" + "/Toilet" + " (" + Random.Range(0, 3).ToString() + ")").position);
                    destinations.Add(Airport.transform.Find(Buildings + "/Bathroom (1)" + "/Sink" + " (" + Random.Range(0, 3).ToString() + ")").position);

                    navMeshAgent.ResetPath();

                    //Randomly wait a time in each 
                    WaitTime.Add(Random.Range(1f, 3f));
                    WaitTime.Add(Random.Range(1f, 3f));
                    break;

                case AgentState.BaggageClaim:
                    break;

                case AgentState.CarRental:
                    break;

                case AgentState.ExitAirport:
                    Destroy(gameObject);
                    break;

                default:
                    break;
            }

            AreaBehaviour = true;
        }
    }
    IEnumerator StateBehavior()
    {
        if(destinations.Count != 0)
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
                    Waiting = true;

                    yield return new WaitForSeconds(WaitTime[0]);
                    WaitTime.Remove(WaitTime[0]);
                    Waiting = false;
                }
            }
        }
        else
        {
            agentState = AgentState.None;
            AreaBehaviour = false;
        }
    }
}


