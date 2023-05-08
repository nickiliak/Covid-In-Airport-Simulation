using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepAgentStats : MonoBehaviour
{
    TextMeshProUGUI Created;
    TextMeshProUGUI Destroyed;
    TextMeshProUGUI Baggage;
    TextMeshProUGUI Restroom;
    TextMeshProUGUI Car;

    void Start()
    {
        //Set our Objects
        Created = transform.Find("Basic Stats/Departing/Created").GetComponent<TextMeshProUGUI>();
        Destroyed = transform.Find("Basic Stats/Departing/Destroyed").GetComponent<TextMeshProUGUI>();
        Baggage = transform.Find("ExtraDeparting/Baggage").GetComponent<TextMeshProUGUI>();
        Restroom = transform.Find("ExtraDeparting/Restroom").GetComponent<TextMeshProUGUI>();
        Car = transform.Find("ExtraDeparting/Car").GetComponent<TextMeshProUGUI>();
    }

    public void OnDepAgentCreated()
    {
        int number = int.Parse(Created.text);
        number++;
        Created.text = number.ToString();
    }

    public void OnDepAgentDestroyed()
    {
        int number = int.Parse(Destroyed.text);
        number++;
        Created.text = number.ToString();
    }

    public void OnDepAgentBaggage()
    {
        int number = int.Parse(Destroyed.text);
        number++;
        Created.text = number.ToString();
    }

    public void OnDepAgentRestroom()
    {
        int number = int.Parse(Destroyed.text);
        number++;
        Created.text = number.ToString();
    }

    public void OnDepAgentCar()
    {
        int number = int.Parse(Destroyed.text);
        number++;
        Created.text = number.ToString();
    }
}
