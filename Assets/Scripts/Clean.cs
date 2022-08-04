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
                target = null;
            }
        }
    }
}
