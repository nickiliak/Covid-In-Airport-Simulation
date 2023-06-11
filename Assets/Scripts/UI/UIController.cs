using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour

{
    [Space(10)]

    [SerializeField] Button OpenStats;
    [SerializeField] Button CloseStats;
    [SerializeField] GameObject StatsWindow;

    [Space(10)]

    [SerializeField] Button OpenActs;
    [SerializeField] Button CloseActs;
    [SerializeField] GameObject ActsWindow;

    [Space(10)]

    [SerializeField] Button OpenCamera;
    [SerializeField] Button CloseCamera;
    [SerializeField] GameObject CameraWindow;

    void Start()
    {
        StatsWindow.SetActive(false);
        OpenStats.onClick.AddListener(OpenStatsWindow); // add the PrintMessage function to the button's onClick event
        CloseStats.onClick.AddListener(CloseStatsWindow); // add the PrintMessage function to the button's onClick event

        ActsWindow.SetActive(false);
        OpenActs.onClick.AddListener(OpenActsWindow); // add the PrintMessage function to the button's onClick event
        CloseActs.onClick.AddListener(CloseActsWindow); // add the PrintMessage function to the button's onClick event

        CameraWindow.SetActive(false);
        OpenCamera.onClick.AddListener(OpenCameraWindow); // add the PrintMessage function to the button's onClick event
        CloseCamera.onClick.AddListener(CloseCameraWindow); // add the PrintMessage function to the button's onClick event
    }
    void OpenStatsWindow()
    {
        StatsWindow.SetActive(true);

        ActsWindow.SetActive(false);
        CameraWindow.SetActive(false);
    }

    void OpenActsWindow()
    {
        ActsWindow.SetActive(true);

        StatsWindow.SetActive(false);
        CameraWindow.SetActive(false);
    }

    void OpenCameraWindow()
    {
        CameraWindow.SetActive(true);

        StatsWindow.SetActive(false);
        ActsWindow.SetActive(false);
    }

    void CloseStatsWindow()
    {
        StatsWindow.SetActive(false);
    }

    void CloseActsWindow()
    {
        ActsWindow.SetActive(false);
    }


    void CloseCameraWindow()
    {
        CameraWindow.SetActive(false);
    }
}
