using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class ChangeState : MonoBehaviour
    {
        [SerializeField] SpotStateManager spot;
        [SerializeField] FacilityStateManager facility;
        [SerializeField] Material openMaterial;
        [SerializeField] Material closedMaterial;
        [SerializeField] Material occupiedMaterial;
        Renderer siteVisual;

        void OnEnable()
        {
            spot.Full.OnStateEnter += OnFacilityFull;
            spot.Vacant.OnStateEnter += OnFacilityVacant;
            facility.Closed.OnStateEnter += OnFacilityClosed;
            facility.Operating.OnStateEnter += OnFacilityOperating;
        }

        void OnDisable()
        {
            spot.Full.OnStateExit -= OnFacilityFull;
            spot.Vacant.OnStateEnter -= OnFacilityVacant;
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
            siteVisual.material = openMaterial;
        }

        private void OnFacilityClosed(FiniteStateManager manager)
        {
            siteVisual.material = closedMaterial;
        }
    }
}
