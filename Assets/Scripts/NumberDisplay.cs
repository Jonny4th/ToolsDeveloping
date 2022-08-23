using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolTesting;
using TMPro;

public class NumberDisplay : MonoBehaviour
{
    [SerializeField] private FacilityStatValue facility;
    [SerializeField] private TMP_Text display;

    private void Update() {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        display.text = facility.value + "/" + facility.valueMax;
    }
}
