using FacilityRelated.Stat;
using System.Collections;
using ToolTesting;
using UnityEngine;

namespace Commands
{
    public class CleanAction : IExecutable
    {
        private float fillingRate; // value increased per sec.
        public float FillingRate { get { return fillingRate; } }
        private GameObject target;

        public IEnumerator CleanCoroutine(GameObject target)
        {
            FacilityCleanlinessStat statValue = target.GetComponentInParent<FacilityCleanlinessStat>();
            bool isDone = false;

            void Done(FacilityStat stat)
            {
                isDone = true;
            }

            statValue.OnValueIsFull += Done;

            while(!isDone)
            {
                target.GetComponentInParent<ICleanable>()?.GetClean(1);
                yield return new WaitForSecondsRealtime(1f / fillingRate);
            }

            statValue.OnValueIsFull -= Done;
        }

        public void Execute()
        {

        }
    }
}
