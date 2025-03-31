using FiniteStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FacilityRelated
{
    public class SpotDependentGroup : MonoBehaviour
    {
        [SerializeField] List<Spot> spots;
        HashSet<SpotStateManager> occupiedSpots = new();
        [SerializeField] int occupantLimit = 1;

        void OnEnable()
        {
            spots = GetComponentsInChildren<Spot>().ToList();

            foreach (var spot in spots)
            {
                spot.state.Occupied.OnStateEnter += OnSpotOccupied;
                spot.state.Occupied.OnStateExit += OnSpotUnccupied;
            }
        }

        void OnDisable()
        {
            foreach(var spot in spots)
            {
                spot.state.Occupied.OnStateEnter -= OnSpotOccupied;
                spot.state.Occupied.OnStateExit -= OnSpotUnccupied;
            }
        }

        private void OnSpotOccupied(FiniteStateManager state)
        {
            if(occupiedSpots.Count == occupantLimit) return;

            occupiedSpots.Add((SpotStateManager)state);

            if(occupiedSpots.Count != occupantLimit) return;

            spots.FindAll(spot => spot.state.CurrentState is not SpotStateManager.OccupiedState)
                .ForEach(freeSpot => freeSpot.state.SetActive(false));
        }
       
        private void OnSpotUnccupied(FiniteStateManager state)
        {
            occupiedSpots.Remove((SpotStateManager)state);

            if(occupiedSpots.Count != occupantLimit - 1) return;

            spots.FindAll(spot => spot.state.CurrentState is not SpotStateManager.OccupiedState)
                .ForEach(freeSpot => freeSpot.state.SetActive(true));
        }
    }
}