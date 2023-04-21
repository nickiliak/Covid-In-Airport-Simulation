using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crowd : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        FindeTarget();
    }

    // Update is called once per frame
    void Update()
    {
        FindeTarget();
    }

    public void FindeTarget()
    {
        navMeshAgent.destination = Target.transform.position;
    }
}
