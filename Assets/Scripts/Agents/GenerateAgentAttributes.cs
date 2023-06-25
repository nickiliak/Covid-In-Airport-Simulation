using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateAgentAttributes : MonoBehaviour
{
    //Attributes
    [SerializeField] int Age;
    [SerializeField] int Gender;
    [SerializeField] float Speed;
    [SerializeField] float Accelaration;
    [SerializeField] float Proximity;

    public NavMeshAgent navMeshAgent;
    public float HeatMapTimer;
    public int HeatMapArrayPos;
    //Health status

    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(4f, 8f);
        Accelaration = Random.Range(4f, 10f);
        Proximity = Random.Range(0.5f, 1f);

        navMeshAgent.speed = Speed;
        navMeshAgent.acceleration = Accelaration;  
        navMeshAgent.radius= Proximity;
        HeatMapTimer = Time.time;
        HeatMapArrayPos = -1;
    }

}
