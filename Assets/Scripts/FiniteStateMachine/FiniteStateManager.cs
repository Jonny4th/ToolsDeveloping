using System;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class FiniteStateManager : MonoBehaviour
    {
        public State CurrentState { get; protected set; }

        public void SwitchState(State state)
        {
            CurrentState.ExitState(this);
            CurrentState = state;
            CurrentState.EnterState(this);
        }
    }

    public abstract class State
    {
        public event Action<FiniteStateManager> OnStateEnter;
        public event Action<FiniteStateManager> OnStateExit;
        
        public virtual void EnterState(FiniteStateManager manager) 
        {
            OnStateEnter?.Invoke(manager);
        }

        public virtual void UpdateState(FiniteStateManager manager) { }

        public virtual void ExitState(FiniteStateManager manager) 
        {
            OnStateExit?.Invoke(manager);
        }
    }
}