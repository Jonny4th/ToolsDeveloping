using System;
using ToolTesting;
using UnityEngine;

namespace FacilityRelated
{
    public class FacilityStatValue : MonoBehaviour, ICleanable
    {
        [SerializeField]
        private int currentValue;
        [SerializeField]
        private int valueMax;
        public bool DoNeedReplenish => CurrentValue <= 0; //{ get; private set; }
        public bool IsFull => CurrentValue == ValueMax;

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = value;
                if(currentValue > valueMax)
                {
                    currentValue = valueMax;
                }
                else if(currentValue < 0)
                {
                    currentValue = 0;
                }
            }
        }

        public int ValueMax { get => valueMax; set => valueMax = value; }

        public event Action OnValueChange;
        public event Action OnValueIsZero;
        public event Action OnValueIsReplenished;
        public event Action OnValueIsFull;

        private void OnEnable()
        {
            OnValueChange += UpdateDoNeedClean;
        }

        private void OnDisable()
        {
            OnValueChange -= UpdateDoNeedClean;
        }

        void Start()
        {
            CurrentValue = ValueMax;
        }

        private void UpdateDoNeedClean()
        {
            if(DoNeedReplenish)
            {
                CurrentValue = 0;
                OnValueIsZero?.Invoke();
            }
            else if(CurrentValue > 0)
            {
                OnValueIsReplenished?.Invoke();
            }
            else if(IsFull)
            {
                OnValueIsFull?.Invoke();
            }
        }

        public void DeductValue()
        {
            if(CurrentValue > 0)
            {
                CurrentValue--;
                OnValueChange?.Invoke();
            }
        }

        void ICleanable.GetClean(int cleaningRate)
        {
            CurrentValue += cleaningRate;
            OnValueChange?.Invoke();
        }

        private void OnApplicationQuit()
        {
            StopAllCoroutines();
        }
    }

}
