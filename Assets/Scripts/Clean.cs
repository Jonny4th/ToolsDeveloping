using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class Clean : MonoBehaviour
    {
        
        [SerializeField] private int FillingRate; // value increased per sec.
        public int fillingRate {get {return FillingRate;}}
        private GameObject target;
        IEnumerator CleanCoroutine(GameObject target)
        {
            Debug.Log("Attemp Coroutine.");
            while(target.GetComponentInParent<FacilityStatValue>().doNeedReplenish)
            {
                Debug.Log("Enter Coroutine.");
                target.GetComponentInParent<ICleanable>()?.GetClean(fillingRate);
                yield return new WaitForSecondsRealtime(1f);
            }
        }
        private IEnumerator coroutine;
        private void OnTriggerEnter(Collider other)
        {
            if(target == null & other.gameObject.GetComponentInParent<ICleanable>() != null)
            {
                Debug.Log("Successfully Enter.");
                target = other.gameObject;
                coroutine = CleanCoroutine(target);
                StartCoroutine(coroutine);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (target != null & other.gameObject == target)
            {
                StopCoroutine(coroutine);
                // coroutine = null;
                target = null;
            }
        }
    }
}
