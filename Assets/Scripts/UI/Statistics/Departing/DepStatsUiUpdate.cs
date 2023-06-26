using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DepStatsUiUpdate : MonoBehaviour
{
    TextMeshProUGUI Created;
    TextMeshProUGUI Destroyed;
    TextMeshProUGUI Baggage;
    TextMeshProUGUI Restroom;
    TextMeshProUGUI Car;

    void Start()
    {
        //Set our Objects
        Created = transform.Find("Basic Stats/Outgoing/Created").GetComponent<TextMeshProUGUI>();
        Destroyed = transform.Find("Basic Stats/Outgoing/Destroyed").GetComponent<TextMeshProUGUI>();
        Baggage = transform.Find("ExtraOutgoing/Baggage/Number").GetComponent<TextMeshProUGUI>();
        Restroom = transform.Find("ExtraOutgoing/Restroom/Number").GetComponent<TextMeshProUGUI>();
        Car = transform.Find("ExtraOutgoing/Car/Number").GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        DepStatsData DepStats = GameObject.Find("DepartingData").GetComponent<DepStatsData>();
        OnDepAgentCreated(DepStats.Created);
        OnDepAgentDestroyed(DepStats.Destroyed);
        OnDepAgentBaggage(DepStats.Baggage);
        OnDepAgentRestroom(DepStats.Restroom);
        OnDepAgentCar(DepStats.Car);
    }

    public void OnDepAgentCreated(int data)
    {
        if (Created == null) return;
        Created.text = data.ToString();
    }

    public void OnDepAgentDestroyed(int data)
    {
        if (Destroyed == null) return;
        Destroyed.text = data.ToString();
    }

    public void OnDepAgentBaggage(int data)
    {
        if (Baggage == null) return;
        Baggage.text = data.ToString();
    }

    public void OnDepAgentRestroom(int data)
    {
        if (Restroom == null) return;
        Restroom.text = data.ToString();
    }

    public void OnDepAgentCar(int data)
    {
        if (Car == null) return;
        Car.text = data.ToString();
    }
}
