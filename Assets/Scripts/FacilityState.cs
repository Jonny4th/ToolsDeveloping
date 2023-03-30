using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FacilityState : MonoBehaviour
{
    [SerializeField] FacilityStates Operating;
    [SerializeField] FacilityStates Occupied;
    [SerializeField] FacilityStates Closed;

    public FacilityStates currentState;
    public event Action<FacilityStates> OnStateChange;

    private void OnEnable()
    {
        currentState = Operating;
    }

    private void StateLogic()
    {

    }

    public void SetState(FacilityStates state)
    {
        currentState = state;
        OnStateChange?.Invoke(state);
    }
}
