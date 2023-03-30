using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class ChangeState : MonoBehaviour
    {
        [SerializeField] private Material openMaterial;
        [SerializeField] private Material closedMaterial;
        [SerializeField] private Material occupiedMaterial;
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
            site.GetComponent<TriggerEnterExitEvents>().OnUserEnter += VisitorEnterReact;
            site.GetComponent<TriggerEnterExitEvents>().OnUserLeave += VisitorLeaveReact;
            gameObject.GetComponentInParent<FacilityStatValue>().OnValueIsZero += AvailabilityToggle;
            gameObject.GetComponentInParent<FacilityStatValue>().OnValueIsFull += AvailabilityToggle;
        }

        void OnDisable()
        {
            site.GetComponent<TriggerEnterExitEvents>().OnUserEnter -= VisitorEnterReact;
            site.GetComponent<TriggerEnterExitEvents>().OnUserLeave -= VisitorLeaveReact;
            gameObject.GetComponentInParent<FacilityStatValue>().OnValueIsZero -= AvailabilityToggle;
            gameObject.GetComponentInParent<FacilityStatValue>().OnValueIsFull -= AvailabilityToggle;
        }

        void Update()
        {
            switch(state)
            {
                case States.Open :
                    gameObject.GetComponent<Renderer>().material = openMaterial;
                    return;
                case States.Occupied :
                    gameObject.GetComponent<Renderer>().material = occupiedMaterial;
                    return;
                case States.Closed :
                    gameObject.GetComponent<Renderer>().material = closedMaterial;
                    return;
            }
        }

        private void VisitorEnterReact(GameObject visitor)
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
