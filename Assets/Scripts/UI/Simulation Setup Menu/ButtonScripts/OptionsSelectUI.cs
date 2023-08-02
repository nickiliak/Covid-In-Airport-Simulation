using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSelectUI : MonoBehaviour
{
    [SerializeField] Button OpenAgentMenuButton;
    [SerializeField] Button OpenVirusMenuButton;

    [SerializeField] GameObject AgentMenu;
    [SerializeField] GameObject VirusMenu;

    private void Start()
    {
        OpenAgentMenuButton.onClick.AddListener(OpenAgentMenu); // add the PrintMessage function to the button's onClick event
        OpenVirusMenuButton.onClick.AddListener(OpenVirusMenu); // add the PrintMessage function to the button's onClick event
    }

    void OpenAgentMenu()
    {
        AgentMenu.SetActive(true);
        VirusMenu.SetActive(false);
    }

    void OpenVirusMenu()
    {
        AgentMenu.SetActive(false);
        VirusMenu.SetActive(true);
    }
}
