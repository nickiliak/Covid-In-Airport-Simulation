using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentData : MonoBehaviour
{
    enum gender { Male, Female }
    public enum agentType { Incoming, Outgoing }

    [Header("Personal Data")]
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] int Age;
    [SerializeField] gender Gender;
    public agentType AgentType;

    [Header("Movement Data")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float Speed;
    [SerializeField] float Accelaration;
    [SerializeField] float Proximity;
    [SerializeField] float Drag;
    [SerializeField] float AngularDrag;

    [Header("Visualization Data")]
    public int CrowdDensityPos;
    public float HeatmapTimer;


    private GenerateAgentData Data = new();
    

    void Start()
    {
        SetPersonalData();
        SetMovementData();
        SetVisualizationData();
    }

    void SetPersonalData()
    {
        Age = Data.GenerateAge();
        Gender = (gender)Enum.GetValues(typeof(gender)).GetValue(Data.GenerateGenderValue());
    }

    void SetMovementData()
    {
        Speed = Data.GenerateSpeed();
        Accelaration = Data.GenerateAccelaration();
        Proximity = Data.GenerateProximity();
        Drag = Data.GenerateDrag();
        AngularDrag = Data.GenerateAngularDrag();

        navMeshAgent.speed = Speed;
        navMeshAgent.acceleration = Accelaration;
        navMeshAgent.radius = Proximity;

        rb.drag = Drag;
        rb.angularDrag = AngularDrag;
    }

    void SetVisualizationData()
    {
        CrowdDensityPos = -1;
        HeatmapTimer = Time.time;
    }
}