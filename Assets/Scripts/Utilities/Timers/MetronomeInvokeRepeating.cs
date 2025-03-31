using UnityEngine;

public class MetronomeInvokeRepeating : MetronomeStrategy
{
    public override void Activate()
    {
        base.Activate();

        InvokeRepeating(nameof(Tick), context.StartDelay, 60f / context.BPM);
    }

    public override void Deactivate()
    {
        CancelInvoke();
    }

    private void Tick()
    {
        context.Tick();
    }
}
