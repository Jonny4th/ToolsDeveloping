using AgentRelated;
using FacilityRelated;
using UnityEngine;

namespace FiniteStateMachine
{
    public class SpotStateManager : FiniteStateManager
    {
        public AvailableState Available { get; private set; } = new();
        public OccupiedState Occupied { get; private set; } = new();
        public InactiveState Inactive { get; private set; } = new();

        public Person CurrentUser { get; private set; } = null;

        void Start()
        {
            CurrentState = Available;
            CurrentState.EnterState(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            ((SpotState)CurrentState).OnPersonEnter(this, other);
        }

        private void OnTriggerExit(Collider other)
        {
            ((SpotState)CurrentState).OnPersonExit(this, other);
        }

        private void SetUser(Person person)
        {
            CurrentUser = person;
        }

        public void SetActive(bool v)
        {
            if(v)
            {
                SwitchState(Available);
            }
            else
            {
                SwitchState(Inactive);
            }
        }

        public abstract class SpotState : State
        {
            public virtual void OnPersonEnter(SpotStateManager spot, Collider other) { }

            public virtual void OnPersonExit(SpotStateManager spot, Collider other) { }
        }

        public class OccupiedState : SpotState
        {
            public override void OnPersonExit(SpotStateManager spot, Collider other)
            {
                if(!other.TryGetComponent(out Person newPerson)) return;

                if(newPerson != spot.CurrentUser) return;

                spot.SwitchState(spot.Available);
            }
        }

        public class AvailableState : SpotState
        {
            public override void EnterState(FiniteStateManager manager)
            {
                ((SpotStateManager)manager).SetUser(null);
                base.EnterState(manager);
            }

            public override void OnPersonEnter(SpotStateManager spot, Collider other)
            {
                if(spot.CurrentUser != null) return;

                if(!other.TryGetComponent(out Person person)) return;

                spot.SetUser(person);
                spot.SwitchState(spot.Occupied);
            }
        }

        public class InactiveState : SpotState
        {

        }
    }
}