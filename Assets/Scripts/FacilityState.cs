using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FacilityState : MonoBehaviour
{
    [SerializeField] FacilityStates[] facilityStates;
    public FacilityStates currentState;
    public event Action<FacilityStates> OnStateChange;

    private void StateLogic()
    {
        
    }


    public void SetState(FacilityStates state)
    {
        currentState = state;
        OnStateChange?.Invoke(state);
    }
    public bool CompareState(FacilityStates state)
    {
        return (currentState == state);
    }
}
