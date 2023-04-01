using System;
using System.Collections;
using UnityEngine;

namespace FacilityRelated.Stat
{
    public class FacilityStat : MonoBehaviour
    {
        [SerializeField]
        protected int currentValue;
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

        [SerializeField]
        protected int valueMax;
        public int ValueMax { get => valueMax; set => valueMax = value; }
        
        public bool IsFull => CurrentValue == ValueMax;

        public event Action<FacilityStat> OnValueChange;
        public event Action<FacilityStat> OnValueIsZero;
        public event Action<FacilityStat> OnValueIsFull;

        public virtual void ValueChanged(FacilityStat stat)
        {
            OnValueChange?.Invoke(stat);
        }

        public virtual void ValueIsZero(FacilityStat stat) 
        {
            OnValueIsZero?.Invoke(stat);
        }

        public void ValueIsFull(FacilityStat stat)
        {
            OnValueIsFull?.Invoke(stat);
        }
    }
}