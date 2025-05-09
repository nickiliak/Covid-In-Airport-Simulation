using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour

{
    [Space(10)]

    [SerializeField] Button OpenStats;
    [SerializeField] Button CloseStats;
    [SerializeField] GameObject StatsWindow;

    [Space(10)]

    [SerializeField] Button OpenCamera;
    [SerializeField] Button CloseCamera;
    [SerializeField] GameObject CameraWindow;

    [Space(10)]

    [SerializeField] Button OnoffCrowdDensity;
    [SerializeField] GameObject CrowdDensity;
    [SerializeField] GameObject AirportPlane;

    [Space(10)]
    [SerializeField] Button OnOffHeatmap;
    [SerializeField] GameObject Heatmap;



    AgentCounter[] AgentCounters;
    void Start()
    {
        AgentCounters = FindObjectsOfType<AgentCounter>();

        StatsWindow.SetActive(false);
        OpenStats.onClick.AddListener(OpenStatsWindow); // add the PrintMessage function to the button's onClick event
        //CloseStats.onClick.AddListener(CloseStatsWindow); // add the PrintMessage function to the button's onClick event

        CameraWindow.SetActive(false);
        OpenCamera.onClick.AddListener(OpenCameraWindow); // add the PrintMessage function to the button's onClick event
        CloseCamera.onClick.AddListener(CloseCameraWindow); // add the PrintMessage function to the button's onClick event

        OnoffCrowdDensity.onClick.AddListener(OnOffCrowdDensityFunc);
        OnOffHeatmap.onClick.AddListener(OnOffHeatmapFunc);
    }
    void OpenStatsWindow()
    {
        StatsWindow.SetActive(true);
        CameraWindow.SetActive(false);
    }

    void OpenCameraWindow()
    {
        CameraWindow.SetActive(true);
        StatsWindow.SetActive(false);    
    }

    void CloseStatsWindow()
    {
        StatsWindow.SetActive(false);
    }

    void CloseCameraWindow()
    {
        CameraWindow.SetActive(false);
    }

    void ActivateProperColliders(bool On)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Agent");
        foreach (GameObject obj in objectsWithTag)
        {
            // Get all capsule collider components found in the children
            CapsuleCollider capsuleCollider = obj.GetComponent<CapsuleCollider>();
            if (On == true) capsuleCollider.isTrigger = false;
            else capsuleCollider.isTrigger = true;

        }
    }

    void OnOffCrowdDensityFunc()
    {
        if(CrowdDensity.activeInHierarchy)
        {
            Color greenColor = Color.red;
            Image buttonImage = OnoffCrowdDensity.GetComponent<Image>();
            buttonImage.color = greenColor;

            CrowdDensity.SetActive(false);
            AirportPlane.SetActive(true);

            ActivateProperColliders(false);

            foreach (AgentCounter AgentCounter_ in AgentCounters)
            {
                AgentCounter_.objectsTouchingFloor = 0;
            }
        }
        else
        {
            Color greenColor = Color.green;
            Image buttonImage = OnoffCrowdDensity.GetComponent<Image>();
            buttonImage.color = greenColor;

            ActivateProperColliders(true);

            AirportPlane.SetActive(false);
            CrowdDensity.SetActive(true);
        }
    }

    void OnOffHeatmapFunc()
    {
        ParticleSystem HeatmapParticleSystem = Heatmap.GetComponent<ParticleSystem>();
        Renderer particleRenderer = HeatmapParticleSystem.GetComponent<Renderer>();
        

        if (particleRenderer.enabled)
        {
            Color greenColor = Color.red;
            Image buttonImage = OnoffCrowdDensity.GetComponent<Image>();
            buttonImage.color = greenColor;

            particleRenderer.enabled = false;

            CrowdDensity.SetActive(false);
            AirportPlane.SetActive(true);

            foreach (AgentCounter AgentCounter_ in AgentCounters)
            {
                AgentCounter_.objectsTouchingFloor = 0;
            }
        }
        else
        {
            Color greenColor = Color.green;
            Image buttonImage = OnoffCrowdDensity.GetComponent<Image>();
            buttonImage.color = greenColor;

            HeatmapController HmC = Heatmap.GetComponent<HeatmapController>();
            HmC.AddSelectedEventsToHeatmap();

            particleRenderer.enabled = true;
            AirportPlane.SetActive(false);
            CrowdDensity.SetActive(false);
        }
    }
}
