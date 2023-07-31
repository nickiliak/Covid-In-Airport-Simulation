using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrStatsData : MonoBehaviour
{
    public int Created = 0;
    public int Destroyed = 0;
    public int CheckIn = 0;
    public int Restroom = 0;
    public int Shop = 0;
    public int Eat = 0;

    Transform Canvas;
    ArrStatsUiUpdate SUU;
    void Start()
    {
       // Canvas = GameObject.Find("Menus").transform;
     //   SUU = Canvas.Find("Statistics").gameObject.GetComponent<ArrStatsUiUpdate>();
    }

    public void OnArrAgentCreated()
    {
        Created++;
        if (SUU != null && SUU.isActiveAndEnabled) { SUU.OnArrAgentCreated(Created); }
    }

    public void OnArrAgentDestroyed()
    {
        Destroyed++;
        if (SUU != null && SUU.isActiveAndEnabled) { SUU.OnArrAgentDestroyed(Destroyed); }
    }

    public void OnArrAgentCheckIn()
    {
        CheckIn++;
        if (SUU != null && SUU.isActiveAndEnabled) { SUU.OnArrAgentCheckIn(CheckIn); }
    }

    public void OnArrAgentRestroom()
    {
        Restroom++;
        if (SUU != null && SUU.isActiveAndEnabled) { SUU.OnArrAgentRestroom(Restroom); }
    }

    public void OnArrAgentShop()
    {
        Shop++;
        if (SUU != null && SUU.isActiveAndEnabled) { SUU.OnArrAgentShop(Shop); }
    }

    public void OnArrAgentEat()
    {
        Eat++;
        if (SUU != null && SUU.isActiveAndEnabled) { SUU.OnArrAgentEat(Eat); }
    }
}
