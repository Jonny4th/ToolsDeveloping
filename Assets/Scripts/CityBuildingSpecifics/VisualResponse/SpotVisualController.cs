using FiniteStateMachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FacilityRelated
{
    public class SpotVisualController : MonoBehaviour
    {
        [SerializeField] Spot spot;
        [SerializeField] Image image;

        void OnEnable()
        {
            spot.state.Available.OnStateEnter += OnAvailable;
            spot.state.Occupied.OnStateEnter += OnOccupied;
            spot.state.Inactive.OnStateEnter += OnInactive;
        }

        void OnDisable()
        {
            spot.state.Available.OnStateEnter -= OnAvailable;
            spot.state.Occupied.OnStateEnter -= OnOccupied;
            spot.state.Inactive.OnStateEnter -= OnInactive;
        }

        private void OnInactive(FiniteStateManager obj)
        {
            image.color = Color.red;
        }

        private void OnOccupied(FiniteStateManager obj)
        {
            image.color = Color.yellow;
        }

        private void OnAvailable(FiniteStateManager obj)
        {
            image.color = Color.white;
        }
    }
}