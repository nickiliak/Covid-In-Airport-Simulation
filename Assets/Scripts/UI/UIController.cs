using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour

{
    public Button OpenStats;
    public Button CloseStats;
    public GameObject StatsWindow;
    

    void Start()
    {
        //StatsWindow.SetActive(false);
        OpenStats.onClick.AddListener(OpenWindow); // add the PrintMessage function to the button's onClick event
        CloseStats.onClick.AddListener(CloseWindow); // add the PrintMessage function to the button's onClick event
    }

    void OpenWindow()
    {
        StatsWindow.SetActive(true);  
    }
    void CloseWindow()
    {
        StatsWindow.SetActive(false);
    }
}
