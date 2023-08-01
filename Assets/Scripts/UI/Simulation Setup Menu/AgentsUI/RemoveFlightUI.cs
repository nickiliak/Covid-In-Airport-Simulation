using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoveFlightUI : MonoBehaviour
{
    public TMP_Dropdown tmpDropdown;
    public Button Remove;
    public GameObject InputFlightFields;


    private void Start()
    {
        Remove.onClick.AddListener(RemoveOption); // add the PrintMessage function to the button's onClick event
    }

    private void RemoveOption()
    {
        int selectedValue = tmpDropdown.value;
        string selectedOptionName = tmpDropdown.options[selectedValue].text;
        tmpDropdown.options.RemoveAt(selectedValue);
        tmpDropdown.value = selectedValue - 1;

        Destroy(InputFlightFields.transform.Find(selectedOptionName).gameObject);
    }
}
