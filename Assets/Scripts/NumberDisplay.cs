using TMPro;
using UnityEngine;
using FacilityRelated;

public class NumberDisplay : MonoBehaviour
{
    [SerializeField] private FacilityStatValue facility;
    [SerializeField] private TMP_Text display;

    private void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        display.text = facility.CurrentValue + "/" + facility.ValueMax;
    }
}
