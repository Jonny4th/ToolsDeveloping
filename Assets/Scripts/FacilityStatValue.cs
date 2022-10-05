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
        public event Action OnValueChange;
        public event Action OnValueIsZero;
        public event Action OnValueIsFull;
        // [SerializeField] private ProgressDisplay progressDisplay;

        private void OnEnable()
        {
            OnValueChange += UpdateDoNeedClean;
            gameObject.GetComponentInChildren<TriggerEnterExitEvents>().OnUserLeave += DeductValue;
        }

        private void OnDisable()
        {
            OnValueChange -= UpdateDoNeedClean;
            gameObject.GetComponentInChildren<TriggerEnterExitEvents>().OnUserLeave -= DeductValue;
        }

        void Start()
        {
            value = valueMax;
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
            if (value > 0)
            {
                value--;
                OnValueChange?.Invoke();
            }
        }

        void ICleanable.GetClean(int cleaningRate)
        {
            value += cleaningRate;
            OnValueChange?.Invoke();
        }
        
        private void OnApplicationQuit()
        {
            StopAllCoroutines();
        }
    }

}
