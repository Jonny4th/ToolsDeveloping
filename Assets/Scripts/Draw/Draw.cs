using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Draw : MonoBehaviour
{
    public void OnPress(CallbackContext context)
    {
        var phase = context.phase;
        Debug.Log("Draw event triggered with context: " + context);
    }

    public void OnDrag(CallbackContext context)
    {
        Debug.Log("Drag event triggered with context: " + context);
    }
}
