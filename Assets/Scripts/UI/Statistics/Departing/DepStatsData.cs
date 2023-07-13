using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepStatsData : MonoBehaviour
{
    public int Created = 0;
    public int Destroyed = 0;
    public int Baggage = 0;
    public int Restroom = 0;
    public int Car = 0;

    Transform Canvas;
    DepStatsUiUpdate SUU;
    void Start()
    {
        Canvas = GameObject.Find("InSimulationMenu").transform;
        SUU = Canvas.Find("Statistics").gameObject.GetComponent<DepStatsUiUpdate>();
    }


    public void OnDepAgentCreated()
    {
        Created++;
        if(SUU != null && SUU.isActiveAndEnabled) { SUU.OnDepAgentCreated(Created); }
    }

    public void OnDepAgentDestroyed()
    {
        Destroyed++;
        if(SUU != null && SUU.isActiveAndEnabled) { SUU.OnDepAgentDestroyed(Destroyed); }
    }

    public void OnDepAgentBaggage()
    {
        Baggage++;
        if(SUU != null && SUU.isActiveAndEnabled) { SUU.OnDepAgentBaggage(Baggage); }
    }

    public void OnDepAgentRestroom()
    {
        Restroom++;
        if (SUU != null && SUU.isActiveAndEnabled) { SUU.OnDepAgentRestroom(Restroom); }
    }

    public void OnDepAgentCar()
    {
        Car++;
        if(SUU != null && SUU.isActiveAndEnabled) { SUU.OnDepAgentCar(Car); }
    }
}
