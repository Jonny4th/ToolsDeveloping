using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class FacilityStatValue : MonoBehaviour, ICleanable
    {
        public int value;
        public int valueMax;
        public bool doNeedReplenish;
        public event Action OnValueIsZero;
        public event Action OnValueIsFull;
        // [SerializeField] private ProgressDisplay progressDisplay;

        void Start()
        {
            value = valueMax;
            gameObject.GetComponentInChildren<TriggerEnterExitEvents>().OnUserLeave += DeductValue;
        }

        void Update()
        {
            UpdateDoNeedClean();
        }

        private void UpdateDoNeedClean()
        {
            if (value <= 0) 
            {
                value = 0;
                if (!doNeedReplenish) 
                {
                    OnValueIsZero?.Invoke();
                    doNeedReplenish = true;
                }
            }
            else if (value >= valueMax)
            {
                value = valueMax;
                if (doNeedReplenish)
                {
                    OnValueIsFull?.Invoke();
                    doNeedReplenish = false;
                } 
            }
        }

        private void DeductValue()
        {
            if (value > 0) value--;
        }

        void ICleanable.GetClean(int cleaningRate)
        {
            value += cleaningRate;
        }
        
        private void OnApplicationQuit()
        {
            StopAllCoroutines();
        }
    }

}
