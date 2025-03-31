using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MetronomeStrategy : MonoBehaviour, IActivatable
{
    protected Metronome context;

    public virtual void Activate()
    {
        Deactivate();
    }

    public virtual void Deactivate()
    {

    }

    public void SetContext(Metronome metronome)
    {
        context = metronome;
    }
}

public interface IActivatable
{
    public void Activate();
    public void Deactivate();
}
