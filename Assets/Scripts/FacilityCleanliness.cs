using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class FacilityCleanliness : MonoBehaviour, ICleanable
    {
        public int cleanliness;
        public int cleanlinessMax;
        public bool doNeedClean;
        public event Action OnCleanlinessIsZero;
        public event Action OnCleanlinessIsFull;
        [SerializeField] private ProgressDisplay progressDisplay;

        // Start is called before the first frame update
        void Start()
        {
            cleanliness = cleanlinessMax;
            gameObject.GetComponentInChildren<Interact>().OnVisitorEnter += VisitorEnterReact;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDoNeedClean();
        }

        private void UpdateDoNeedClean()
        {
            if (cleanliness <= 0) 
            {
                cleanliness = 0;
                if (!doNeedClean) 
                {
                    OnCleanlinessIsZero?.Invoke();
                    doNeedClean = true;
                }
            }
            else if (cleanliness >= cleanlinessMax)
            {
                cleanliness = cleanlinessMax;
                if (doNeedClean)
                {
                    OnCleanlinessIsFull?.Invoke();
                    doNeedClean = false;
                } 
            }
        }

        private void VisitorEnterReact()
        {
            if (cleanliness > 0) cleanliness--;
        }

        void ICleanable.GetClean(int cleaningRate)
        {
            cleanliness += cleaningRate;
        }
        
        private void OnApplicationQuit()
        {
            StopAllCoroutines();
        }
    }

}
