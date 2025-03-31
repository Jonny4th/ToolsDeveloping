using AgentRelated;
using FacilityRelated;
using FacilityRelated.Stat;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolTesting;
using UnityEngine;

namespace FiniteStateMachine
{
    public class FacilityStateManager : FiniteStateManager, IInteractable
    {
        public OperatingState Operating { get; private set; } = new();
        public ClosedState Closed { get; private set; } = new();

        [SerializeField] Facility facility;
        [SerializeField] List<SpotStateManager> spots;

        void OnEnable()
        {
            facility.Cleanliness.OnValueIsZero += Close;
            facility.Cleanliness.GetReplenished += Open;
        }

        void Awake()
        {
            spots = GetComponentsInChildren<SpotStateManager>().ToList();
        }

        void Start()
        {
            CurrentState = Operating;
            CurrentState.EnterState(this);
        }

        public void Interact(Person person)
        {
            ((FacilityState)CurrentState).Interact(person, this);
        }
        
        public void Close(FacilityStat stat)
        {
            SwitchState(Closed);
        }

        public void Open(FacilityStat stat)
        {
            SwitchState(Operating);
        }

        public abstract class FacilityState : State
        {
            public virtual void Interact(Person person, FacilityStateManager context) { }
        }

        public class OperatingState : FacilityState
        {
            public override void Interact(Person person, FacilityStateManager context)
            {
                if(!context.spots.Exists(spot => spot.CurrentUser == person)) return;
                person.GetComponent<VisitorStatValue>().ModifyValue(5f);
                context.facility.Cleanliness.DeductValue();
            }
        }

        public class ClosedState : FacilityState
        {

        }
    }
}
