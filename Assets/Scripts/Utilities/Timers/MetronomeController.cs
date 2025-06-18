using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeController : MonoBehaviour
{
    public Metronome Metronome;

    public void StartMetronome()
    {
        Metronome.Activate();
    }

    public void StopMetronome()
    {
        Metronome.Deactivate();
    }    
}
