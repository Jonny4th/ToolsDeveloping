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

        private void OnSpotOccupied(FiniteStateManager obj)
        {
            foreach(var spot in spots)
            {
                if(spot.state == obj) continue;
                spot.state.SetActive(false);
            }
        }
       
        private void OnSpotUnccupied(FiniteStateManager obj)
        {
            foreach(var spot in spots)
            {
                if(spot.state == obj) continue;
                spot.state.SetActive(true);
            }
        }
    }
}