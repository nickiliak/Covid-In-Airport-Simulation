using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class AgentCounter : MonoBehaviour
{
    public int objectsTouchingFloor = 0;
    TextMeshPro AgentCounterText;
    string PlaneText;

    void Start()
    {
        GameObject ChildText = transform.Find("AgentCounter").gameObject;
        AgentCounterText = ChildText.GetComponent<TextMeshPro>();

        string pattern = @"\d";
        if(AgentCounterText.text != "0")
        {
            Match match = Regex.Match(AgentCounterText.text, pattern);
            PlaneText = match.Success ? AgentCounterText.text[..match.Index] : AgentCounterText.text;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent")) // Change "Player" tag to your object tag
        {
            objectsTouchingFloor++;

            if (gameObject.name == "Entry Agents Plane") AgentCounterText.text = objectsTouchingFloor.ToString();
            else if (gameObject.name == "Exit Agents Plane") AgentCounterText.text = objectsTouchingFloor.ToString();
            else AgentCounterText.text = PlaneText + objectsTouchingFloor.ToString() + " Agents";

            other.gameObject.GetComponent<AgentData>().CurrentAreaInName = gameObject.name;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Agent")) // Change "Player" tag to your object tag
        {
            objectsTouchingFloor--;

            if (gameObject.name == "Entry Agents Plane") AgentCounterText.text = objectsTouchingFloor.ToString();
            else if (gameObject.name == "Exit Agents Plane") AgentCounterText.text = objectsTouchingFloor.ToString();
            else AgentCounterText.text = PlaneText + objectsTouchingFloor.ToString() + " Agents";

            if (other.gameObject.GetComponent<AgentData>().CurrentAreaInName != "GatesArr Plane (1)")
                other.gameObject.GetComponent<AgentData>().CurrentAreaInName = "null";
        }
    }


}
