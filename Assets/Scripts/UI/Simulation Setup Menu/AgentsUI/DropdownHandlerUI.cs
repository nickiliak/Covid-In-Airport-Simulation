using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownHandlerUI : MonoBehaviour
{
    public TMP_Dropdown tmpDropdown;
    public GameObject InputFlightFields;

    private void Start()
    {
        // Add a listener to the TMP_Dropdown to detect when the user changes the selection
        tmpDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        InputFlightFields.transform.Find("Flight " + (index + 1).ToString()).transform.SetAsLastSibling();
    }
}
