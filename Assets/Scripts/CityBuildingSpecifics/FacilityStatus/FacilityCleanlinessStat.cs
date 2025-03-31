using System;
using ToolTesting;

namespace FacilityRelated.Stat
{
    public class FacilityCleanlinessStat : FacilityStat, ICleanable
    {
        public bool DoNeedReplenish => CurrentValue <= 0;
        public bool GetReplenish;

        public event Action<FacilityStat> GetReplenished; 

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

        private void UpdateDoNeedClean(FacilityStat stat)
        {
            if(stat != this) return;
            if(DoNeedReplenish)
            {
                CurrentValue = 0;
                GetReplenish = false;
                ValueIsZero(this);
                return;
            }
            
            if(!DoNeedReplenish && !IsFull)
            {
                if(GetReplenish) return;
                GetReplenished?.Invoke(this);
                GetReplenish = true;
                return;
            }
            
            if(IsFull)
            {
                ValueIsFull(this);
                return;
            }
        }

        public void DeductValue()
        {
            if(CurrentValue > 0)
            {
                CurrentValue--;
                ValueChanged(this);
            }
        }

        void ICleanable.GetClean(int cleaningRate)
        {
            CurrentValue += cleaningRate;
            ValueChanged(this);
        }

        private void OnApplicationQuit()
        {
            StopAllCoroutines();
        }
    }

}
