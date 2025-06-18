using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class SlicedFiledImageConnector : MonoBehaviour
    {
        public SlicedFilledImage display;
        public MonoBehaviour ValueDisplayableInterface;

        IValueDisplayable value;

        void Awake()
        {
            value = ValueDisplayableInterface as IValueDisplayable;
            value.ValueChanged += UpdateDisplay;
        }

        void UpdateDisplay()
        {
            display.fillAmount = value.CurrentValue / value.MaxValue;
        }
    }
}
