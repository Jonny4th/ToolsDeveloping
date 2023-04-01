using Agent;
using UnityEngine;

namespace FiniteStateMachine
{
    public sealed class SpotStateManager : FiniteStateManager
    {
        public VacantState Vacant { get; private set; } = new();
        public FullState Full { get; private set; } = new();
        public Person CurrentUser { get; private set; } = null;

        void Start()
        {
            CurrentState = Vacant;
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

        public void SetUser(Person person)
        {
            CurrentUser = person;
        }
    }

    public abstract class SpotState : State
    {
        public virtual void OnPersonEnter(SpotStateManager spot, Collider other) { }

        public virtual void OnPersonExit(SpotStateManager spot, Collider other) { }
    }

    public class FullState : SpotState
    {
        public override void OnPersonExit(SpotStateManager spot, Collider other)
        {
            if(!other.TryGetComponent(out Person newPerson)) return;

            if(newPerson != spot.CurrentUser) return;

            spot.SwitchState(spot.Vacant);
        }
    }

    public class VacantState : SpotState
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
            spot.SwitchState(spot.Full);
        }
    }
}