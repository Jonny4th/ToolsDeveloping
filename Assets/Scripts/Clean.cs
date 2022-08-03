using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class Clean : MonoBehaviour
    {
        public int cleaningRate {get; private set;}
        [SerializeField] private int CleaningRate;
        private GameObject target;
        // Start is called before the first frame update
        IEnumerator CleanCoroutine(GameObject target)
        {
            while(target.GetComponentInParent<FacilityCleanliness>().doNeedClean)
            {
                target.GetComponentInParent<ICleanable>()?.GetClean(cleaningRate);
                yield return new WaitForSecondsRealtime(1f);
            }
        }
        private IEnumerator coroutine;
        void Start()
        {
            // target = null;
            cleaningRate = CleaningRate;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(target == null & other.gameObject.GetComponentInParent<FacilityCleanliness>() != null)
            {
                Debug.Log("Trigger Successfull.");
                target = other.gameObject;
                coroutine = CleanCoroutine(target);
                StartCoroutine(coroutine);
                // target?.GetClean(cleaningRate);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (target != null & other.gameObject == target)
            {
                StopCoroutine(coroutine);
                target = null;
                Debug.Log("Stop coroutine.");
            }
        }
    }
}
