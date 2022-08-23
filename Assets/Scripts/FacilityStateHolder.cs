using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FacilityStateHolder : MonoBehaviour
{
    public FacilityStates currentState;
    public static event Action<FacilityStates> OnStateChange;
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
