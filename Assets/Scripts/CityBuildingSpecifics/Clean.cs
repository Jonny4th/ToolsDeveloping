using FacilityRelated.Stat;
using System.Collections;
using ToolTesting;
using UnityEngine;

namespace FacilityRelated
{
    public class Clean : MonoBehaviour
    {
        [SerializeField] private float fillingRate; // value increased per sec.
        public float FillingRate { get { return fillingRate; } }
        private GameObject target;

        IEnumerator CleanCoroutine(GameObject target)
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

        private IEnumerator coroutine;

        private void OnTriggerEnter(Collider other)
        {
            if(target == null & other.gameObject.GetComponentInParent<ICleanable>() != null)
            {
                target = other.gameObject;
                coroutine = CleanCoroutine(target);
                StartCoroutine(coroutine);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(target != null & other.gameObject == target)
            {
                StopCoroutine(coroutine);
                target = null;
            }
        }
    }
}
