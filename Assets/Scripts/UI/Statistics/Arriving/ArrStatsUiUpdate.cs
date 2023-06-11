using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrStatsUiUpdate : MonoBehaviour
{
    TextMeshProUGUI Created;
    TextMeshProUGUI Destroyed;
    TextMeshProUGUI CheckIn;
    TextMeshProUGUI Restroom;
    TextMeshProUGUI Shop;
    TextMeshProUGUI Eat;

    void Start()
    {
        //Set our Objects
        Created = transform.Find("Basic Stats/Arriving/Created").GetComponent<TextMeshProUGUI>();
        Destroyed = transform.Find("Basic Stats/Arriving/Destroyed").GetComponent<TextMeshProUGUI>();
        CheckIn = transform.Find("ExtraArriving/CheckIn/Number").GetComponent<TextMeshProUGUI>();
        Restroom = transform.Find("ExtraArriving/Restroom/Number").GetComponent<TextMeshProUGUI>();
        Shop = transform.Find("ExtraArriving/Shop/Number").GetComponent<TextMeshProUGUI>();
        Eat = transform.Find("ExtraArriving/Ate/Number").GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        ArrStatsData ArrStats = GameObject.Find("ArrivingData").GetComponent<ArrStatsData>();
        OnArrAgentCreated(ArrStats.Created);
        OnArrAgentDestroyed(ArrStats.Destroyed);
        OnArrAgentCheckIn(ArrStats.CheckIn);
        OnArrAgentRestroom(ArrStats.Restroom);
        OnArrAgentShop(ArrStats.Shop);
        OnArrAgentEat(ArrStats.Eat);
    }

    public void OnArrAgentCreated(int data)
    {
        if (Created == null) return;
        Created.text = data.ToString();
    }

    public void OnArrAgentDestroyed(int data)
    {
        if (Destroyed == null) return;
        Destroyed.text = data.ToString();
    }

    public void OnArrAgentCheckIn(int data)
    {
        if (CheckIn == null) return;
        CheckIn.text = data.ToString();
    }

    public void OnArrAgentRestroom(int data)
    {
        if (Restroom == null) return;
        Restroom.text = data.ToString();
    }

    public void OnArrAgentShop(int data)
    {
        if (Shop == null) return;
        Shop.text = data.ToString();
    }
    public void OnArrAgentEat(int data)
    {
        if (Eat == null) return;
        Eat.text = data.ToString();
    }
}
