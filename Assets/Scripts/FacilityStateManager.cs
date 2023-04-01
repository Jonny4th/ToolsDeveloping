using AgentRelated;
using FacilityRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolTesting;
using UnityEngine;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

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
            facility.Cleanliness.OnValueIsReplenished += Open;
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
        
        public void Close()
        {
            SwitchState(Closed);
        }

        public void Open()
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
