using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddFlightUI : MonoBehaviour
{
    public TMP_Dropdown tmpDropdown;
    public Button Add;
    public GameObject InputFlightFields;


    private void Start()
    {
        Add.onClick.AddListener(AddNewOption); // add the PrintMessage function to the button's onClick event
    }

    private void AddNewOption()
    {
        int flightNumber = tmpDropdown.options.Count + 1;
        string newName = "Flight " + flightNumber.ToString();

        tmpDropdown.options.Add(new TMP_Dropdown.OptionData(newName));
        tmpDropdown.value = flightNumber;


        Instantiate(InputFlightFields.transform.GetChild(0), InputFlightFields.transform).name = newName;

    }
}
