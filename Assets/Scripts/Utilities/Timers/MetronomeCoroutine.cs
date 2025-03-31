using System.Collections;
using UnityEngine;

namespace TimeRelated
{
    public class MetronomeCoroutine : MetronomeStrategy
    {
        private Coroutine coroutine = null;

        public override void Activate()
        {
            base.Activate();

            coroutine = StartCoroutine(Timer());
        }

        public override void Deactivate()
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        IEnumerator Timer()
        {
            var period = 60f / context.BPM;

            yield return new WaitForSecondsRealtime(context.StartDelay);

            while(true)
            {
                context.Tick();
                yield return new WaitForSecondsRealtime(period);
            }
        }
    }
}
