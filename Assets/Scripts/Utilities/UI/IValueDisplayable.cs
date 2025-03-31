using System;

public interface IValueDisplayable
{
    public float MaxValue { get; }
    public float CurrentValue { get; }

    public event Action ValueChanged;
}