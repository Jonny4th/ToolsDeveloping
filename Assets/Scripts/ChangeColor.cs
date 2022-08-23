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
            Open,
            Occupied,
            Closed
        }

        public States state = States.Open;
        void OnEnable()
        {
            site.gameObject.GetComponent<GateKeepingEvents>().OnUserEnter += VisitorEnterReact;
            site.gameObject.GetComponent<GateKeepingEvents>().OnUserLeave += VisitorLeaveReact;
            gameObject.GetComponentInParent<FacilityStatValue>().OnValueIsZero += AvailabilityToggle;
            gameObject.GetComponentInParent<FacilityStatValue>().OnValueIsFull += AvailabilityToggle;
        }

        void OnDisable()
        {
            site.gameObject.GetComponent<GateKeepingEvents>().OnUserEnter -= VisitorEnterReact;
            site.gameObject.GetComponent<GateKeepingEvents>().OnUserLeave -= VisitorLeaveReact;
            gameObject.GetComponentInParent<FacilityStatValue>().OnValueIsZero -= AvailabilityToggle;
            gameObject.GetComponentInParent<FacilityStatValue>().OnValueIsFull -= AvailabilityToggle;
        }

        void Update()
        {
            switch(state)
            {
                case States.Open :
                    gameObject.GetComponent<Renderer>().material = activeMaterial;
                    return;
                case States.Occupied :
                    gameObject.GetComponent<Renderer>().material = workingMaterial;
                    return;
                case States.Closed :
                    gameObject.GetComponent<Renderer>().material = inactiveMaterial;
                    return;
            }
        }

        private void VisitorEnterReact()
        {
            if (state == States.Open) state = States.Occupied;
        }

        private void VisitorLeaveReact()
        {
            if (state == States.Occupied) state = States.Open;
        }

        private void AvailabilityToggle()
        {
            state = (state == States.Closed)? States.Open : States.Closed;
        }
    }
}
