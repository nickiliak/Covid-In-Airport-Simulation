using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSelectUI : MonoBehaviour
{
    [SerializeField] Button OpenAgentMenuButton;
    [SerializeField] Button OpenVirusMenuButton;
    [SerializeField] Button OpenLimitPerAreaButton;

    [SerializeField] GameObject AgentMenu;
    [SerializeField] GameObject VirusMenu;
    [SerializeField] GameObject LimitPerAreaMenu;

    private void Start()
    {
        OpenAgentMenuButton.onClick.AddListener(OpenAgentMenu); // add the PrintMessage function to the button's onClick event
        OpenVirusMenuButton.onClick.AddListener(OpenVirusMenu); // add the PrintMessage function to the button's onClick event
        OpenLimitPerAreaButton.onClick.AddListener(OpenLimitPerAreaMenu); // add the PrintMessage function to the button's onClick event
    }

    void OpenAgentMenu()
    {
        AgentMenu.SetActive(true);

        VirusMenu.SetActive(false);
        LimitPerAreaMenu.SetActive(false);
    }

    void OpenVirusMenu()
    {
        VirusMenu.SetActive(true);

        AgentMenu.SetActive(false);
        LimitPerAreaMenu.SetActive(false);
    }

    void OpenLimitPerAreaMenu()
    {
        LimitPerAreaMenu.SetActive(true);

        AgentMenu.SetActive(false);
        VirusMenu.SetActive(false);
    }
}
