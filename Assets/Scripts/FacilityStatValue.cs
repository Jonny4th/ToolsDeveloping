using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class FacilityStatValue : MonoBehaviour, ICleanable
    {
        public int currentValue;
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
            currentValue = valueMax;
        }

        private void UpdateDoNeedClean()
        {
            if (currentValue <= 0) 
            {
                currentValue = 0;
                if (!doNeedReplenish) 
                {
                    OnValueIsZero?.Invoke();
                    doNeedReplenish = true;
                }
            }
            else if (currentValue >= valueMax)
            {
                currentValue = valueMax;
                if (doNeedReplenish)
                {
                    OnValueIsFull?.Invoke();
                    doNeedReplenish = false;
                } 
            }
        }

        public void DeductValue()
        {
            if (currentValue > 0)
            {
                currentValue--;
                OnValueChange?.Invoke();
            }
        }

        void ICleanable.GetClean(int cleaningRate)
        {
            currentValue += cleaningRate;
            OnValueChange?.Invoke();
        }
        
        private void OnApplicationQuit()
        {
            StopAllCoroutines();
        }
    }

}
