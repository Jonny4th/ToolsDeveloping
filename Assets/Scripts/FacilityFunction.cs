using System;
using System.Collections;
using System.Collections.Generic;
using ToolTesting;
using UnityEngine;

public class FacilityFunction : MonoBehaviour
{
    [SerializeField] private TriggerEnterExitEvents[] sites;
    private void OnEnable()
    {
        foreach (var site in sites)
        {
            site.OnUserEnter += DoThingsToVisitor;
        }
    }

    private void OnDisable()
    {
        foreach (var site in sites)
        {
            site.OnUserEnter -= DoThingsToVisitor;
        }
        
    }

    private void DoThingsToVisitor(GameObject visitor)
    {
        var state = GetComponentInChildren<ChangeState>().state;
        if(state == ChangeState.States.Closed) return;
        visitor.GetComponent<VisitorStatValue>().ModifyValue(5f);
    }
}
