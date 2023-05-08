using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;
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
    private string FirstSectionAreas = "First Section/Areas";
    private string SecondSectionAreas = "Second Section/Areas";
    GameObject Stats = GameObject.Find("Statistics");
    DepAgentStats DepStats;

    void Start()
    {
        //Initial State
        agentState = AgentState.None;
        Airport = GameObject.Find("Airport");

        //Get UI to update it
        DepStats = Stats.GetComponent<DepAgentStats>();
        DepStats.OnDepAgentCreated();

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
            navMeshAgent.destination = Airport.transform.Find(SecondSectionAreas + "/Bathroom (1)/BathroomBuild (1)/Target").position;
            NeedsRestroom = false;
        }
        else if (NeedsBaggage)
        {
            agentState = AgentState.BaggageClaim;
            navMeshAgent.destination = Airport.transform.Find(FirstSectionAreas + "/BaggageClaim/Target").position;
            NeedsBaggage = false;
        }
        else if (NeedsCar)
        {
            agentState = AgentState.CarRental;
            NeedsCar = false;
        }
        else
        {
            agentState = AgentState.ExitAirport;

            //Target at the middle of the plane
            Vector3 ExitPosition = Airport.transform.Find("ExitPlane/Target").position;
            //Randomly pick a point of the plane to go to and exit the airport
            navMeshAgent.destination = new Vector3(ExitPosition.x + Random.Range(-70,70), ExitPosition.y, ExitPosition.z);
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
                    destinations.Add(Airport.transform.Find(SecondSectionAreas + 
                        "/Bathroom (1)/BathroomBuild (1)" + "/Toilet" + " (" + Random.Range(0, 3).ToString() + ")").position);
                    destinations.Add(Airport.transform.Find(SecondSectionAreas + 
                        "/Bathroom (1)/BathroomBuild (1)" + "/Sink" + " (" + Random.Range(0, 3).ToString() + ")").position);

                    DepStats.OnDepAgentRestroom();
                    navMeshAgent.ResetPath();

                    //Randomly wait a time in each 
                    WaitTime.Add(Random.Range(1f, 3f));
                    WaitTime.Add(Random.Range(1f, 3f));
                    break;

                case AgentState.BaggageClaim:
                    //Randomly pick one of the conveyor belts
                    destinations.Add(Airport.transform.Find(FirstSectionAreas +
                        "/BaggageClaim/PickUp" + " (" + Random.Range(1, 3).ToString() + ")" +
                        "/Items" + "/SuitCase" + " (" + Random.Range(0, 8).ToString() + ")"
                        ).position);

                    DepStats.OnDepAgentBaggage();
                    navMeshAgent.ResetPath();

                    //Randomly wait a time in each 
                    WaitTime.Add(Random.Range(1f, 2f));
                    WaitTime.Add(Random.Range(1f, 2f));
                    break;

                case AgentState.CarRental:
                    //Randomly pick one of the conveyor belts
                    destinations.Add(Airport.transform.Find(FirstSectionAreas +
                        "/Car Rental/Targets/Target" + " (" + Random.Range(0, 3).ToString() + ")"
                        ).position);

                    DepStats.OnDepAgentCar();
                    navMeshAgent.ResetPath();

                    //Randomly wait a time in each 
                    WaitTime.Add(Random.Range(2f, 4f));
                    WaitTime.Add(Random.Range(2f, 4f));
                    break;

                case AgentState.ExitAirport:

                    DepStats.OnDepAgentDestroyed();
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


