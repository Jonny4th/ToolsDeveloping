using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ToolTesting
{
    public class VisitorStatValue : MonoBehaviour
    {
        private float currentValue;
        [SerializeField] TMP_Text valueDisplay;
     
        private void Start()
        {
            SetValue(0);
        }

        private void DisplayValue()
        {
            valueDisplay.text = currentValue.ToString();
        }

        public void ModifyValue(float value)
        {
            currentValue += value;
            DisplayValue();
        }

        public void SetValue(float value)
        {
            currentValue = value;
            DisplayValue();
        }

    }

}
