using System.Collections;
using UnityEngine;

public class MetronomeDeltaTime : MetronomeStrategy
{
    private bool isActive = false;
    private float period => 60f / context.BPM;
    private float countdown;

    void Update()
    {
        if(!isActive) return;
        Timer();
    }

    private void Timer()
    {
        countdown -= Time.deltaTime;

        if(countdown < 0)
        {
            context.Tick();
            countdown = period;
        }
    }

    public override void Activate()
    {
        base.Activate();

        DelayStart();
    }

    private void SetStart()
    {
        countdown = period;
        isActive = true;
    }

    public override void Deactivate()
    {
        isActive = false;
    }

    public void DelayStart()
    {
        Invoke(nameof(SetStart), context.StartDelay);
    }
}
