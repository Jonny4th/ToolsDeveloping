using System;
using UnityEngine;

[CreateAssetMenu]
public class EventChannel : ScriptableObject
{
    private event Action m_OnEventRaised;

    public void Invoke()
    {
        m_OnEventRaised?.Invoke();
    }

    public void AddListener(Action listener)
    {
        m_OnEventRaised += listener;
    }

    internal void RemoveListener(Action listener)
    {
        m_OnEventRaised -= listener;
    }

    internal void RemoveAllListeners()
    {
        m_OnEventRaised = null;
    }
}
