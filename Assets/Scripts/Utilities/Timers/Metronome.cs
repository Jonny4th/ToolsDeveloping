using UnityEngine;

public class Metronome : MonoBehaviour, IActivatable
{
    [SerializeField]
    private int m_BPM = 60;
    public int BPM { get => m_BPM; private set => m_BPM = value; }

    [SerializeField] 
    private int m_Beats = 4;
    public int Beats { get => m_Beats; private set => m_Beats = value; }

    [SerializeField]
    private float startDelay;
    public float StartDelay { get => startDelay; private set => startDelay = value; }

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip mainBeatClip;
    [SerializeField]
    private AudioClip regularBeatClip;

    public int currentBeat { get; private set; } = 0;

    [SerializeField]
    MetronomeStrategy m_strategy;

    void OnEnable()
    {
        if(m_strategy != null)
        {
            m_strategy.SetContext(this);
        }
    }

    public void Activate()
    {
        currentBeat = 0;
        m_strategy.Activate();
    }

    public void Deactivate() 
    {
        m_strategy.Deactivate();
    }

    public void Tick()
    {
        currentBeat %= m_Beats;
        
        if(currentBeat == 0) 
        {
            audioSource.PlayOneShot(mainBeatClip);
        }
        else
        {
            audioSource.PlayOneShot(regularBeatClip);
        }

        currentBeat++;
    }

    public void SetStrategy(MetronomeStrategy strategy)
    {
        m_strategy = strategy;
        m_strategy.SetContext(this);
    }
}
