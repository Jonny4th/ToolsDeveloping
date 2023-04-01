using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class VisualController : MonoBehaviour
    {
        [SerializeField] SpotStateManager spot;
        [SerializeField] FacilityStateManager facility;
        [SerializeField] Material openMaterial;
        [SerializeField] Material closedMaterial;
        [SerializeField] Material occupiedMaterial;
        Renderer siteVisual;

        void OnEnable()
        {
            spot.Occupied.OnStateEnter += OnFacilityFull;
            spot.Available.OnStateEnter += OnFacilityVacant;
            facility.Closed.OnStateEnter += OnFacilityClosed;
            facility.Operating.OnStateEnter += OnFacilityOperating;
        }

        void OnDisable()
        {
            spot.Occupied.OnStateExit -= OnFacilityFull;
            spot.Available.OnStateEnter -= OnFacilityVacant;
        }

        void Awake()
        {
            siteVisual = GetComponent<Renderer>();
        }

        private void OnFacilityFull(FiniteStateManager stateManager)
        {
            if(facility.CurrentState is not FacilityStateManager.OperatingState) return;
            siteVisual.material = occupiedMaterial;
        }

        private void OnFacilityVacant(FiniteStateManager stateManager)
        {
            if(facility.CurrentState is not FacilityStateManager.OperatingState) return;
            siteVisual.material = openMaterial;
        }

        private void OnFacilityOperating(FiniteStateManager manager)
        {
            if(spot.CurrentState is not SpotStateManager.AvailableState) return;
            siteVisual.material = openMaterial;
        }

        private void OnFacilityClosed(FiniteStateManager manager)
        {
            siteVisual.material = closedMaterial;
        }
    }
}
