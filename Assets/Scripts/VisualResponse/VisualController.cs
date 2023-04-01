using FacilityRelated;
using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class VisualController : MonoBehaviour
    {
        [SerializeField] FacilityStateManager facilitystate;
        
        [SerializeField] Material openMaterial;
        [SerializeField] Material closedMaterial;
        [SerializeField] Material occupiedMaterial;

        Renderer siteVisual;

        void OnEnable()
        {
            facilitystate.Closed.OnStateEnter += OnFacilityClosed;
            facilitystate.Operating.OnStateEnter += OnFacilityOperating;
        }

        void OnDisable()
        {
            facilitystate.Closed.OnStateEnter -= OnFacilityClosed;
            facilitystate.Operating.OnStateEnter -= OnFacilityOperating;
        }

        void Awake()
        {
            siteVisual = GetComponent<Renderer>();
        }

        private void OnFacilityOperating(FiniteStateManager manager)
        {
            siteVisual.material = openMaterial;
        }

        private void OnFacilityClosed(FiniteStateManager manager)
        {
            siteVisual.material = closedMaterial;
        }
    }
}
