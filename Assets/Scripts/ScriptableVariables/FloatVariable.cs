// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    [SerializeField]
    private float m_Value;

    public float Value { get => m_Value; }

    public void SetValue(float value)
    {
        m_Value = value;
    }

    public void SetValue(FloatVariable value)
    {
        m_Value = value.Value;
    }

    public void ApplyChange(float amount)
    {
        m_Value += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        m_Value += amount.Value;
    }
}
