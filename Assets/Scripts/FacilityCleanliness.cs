using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class FacilityCleanliness : MonoBehaviour, ICleanable
    {
        [SerializeField] private int Cleanliness;
        [SerializeField] private int CleanlinessMax;
        [SerializeField] public bool doNeedClean;
        public event Action OnCleanlinessIsZero;
        public event Action OnCleanlinessIsFull;

        // Start is called before the first frame update
        void Start()
        {
            Cleanliness = CleanlinessMax;
            gameObject.GetComponentInChildren<Interact>().OnVisitorEnter += VisitorEnterReact;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDoNeedClean();
        }

        private void UpdateDoNeedClean()
        {
            if (Cleanliness <= 0) 
            {
                Cleanliness = 0;
                if (!doNeedClean) 
                {
                    OnCleanlinessIsZero?.Invoke();
                    doNeedClean = true;
                }
            }
            else if (Cleanliness >= CleanlinessMax)
            {
                Cleanliness = CleanlinessMax;
                if (doNeedClean)
                {
                    OnCleanlinessIsFull?.Invoke();
                    doNeedClean = false;
                } 
            }
        }

        private void VisitorEnterReact()
        {
            if (Cleanliness > 0) Cleanliness--;
        }

        void ICleanable.GetClean(int cleaningRate)
        {
            // IEnumerator CleanCoroutine()
            // {
            //     while(doNeedClean)
            //     {
            //         Cleanliness += cleaningRate;
            //         yield return new WaitForSecondsRealtime(1f);
            //     }
            // }
            // StartCoroutine(CleanCoroutine());
            Cleanliness += cleaningRate;
        }
        
        private void OnApplicationQuit()
        {
            StopAllCoroutines();
        }
    }

}
