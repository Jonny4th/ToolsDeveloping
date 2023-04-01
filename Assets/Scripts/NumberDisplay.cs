using FacilityRelated.Stat;
using TMPro;
using UnityEngine;

public class NumberDisplay : MonoBehaviour
{
    [SerializeField] private FacilityCleanlinessStat facility;
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
