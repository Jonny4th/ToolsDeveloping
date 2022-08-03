using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class ChangeColor : MonoBehaviour
    {
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material inactiveMaterial;
        [SerializeField] private Material workingMaterial;
        [SerializeField] private GameObject site;

        public enum States 
        {
            Active,
            Working,
            Inactive
        }

        public States state = States.Active;
        void Start()
        {
            site.gameObject.GetComponent<Interact>().OnVisitorEnter += VisitorEnterReact;
            site.gameObject.GetComponent<Interact>().OnVisitorLeave += VisitorLeaveReact;
            gameObject.GetComponentInParent<FacilityCleanliness>().OnCleanlinessIsZero += AvailabilityToggle;
            gameObject.GetComponentInParent<FacilityCleanliness>().OnCleanlinessIsFull += AvailabilityToggle;
        }

        void Update()
        {
            switch(state)
            {
                case States.Active :
                    gameObject.GetComponent<Renderer>().material = activeMaterial;
                    return;
                case States.Working :
                    gameObject.GetComponent<Renderer>().material = workingMaterial;
                    return;
                case States.Inactive :
                    gameObject.GetComponent<Renderer>().material = inactiveMaterial;
                    return;
            }
        }

        private void VisitorEnterReact()
        {
            if (state == States.Active) state = States.Working;
            
        }

        private void VisitorLeaveReact()
        {
            if (state == States.Working) state = States.Active;
        }

        private void AvailabilityToggle()
        {
            state = (state == States.Inactive)? States.Active : States.Inactive;
        }
    }
}
