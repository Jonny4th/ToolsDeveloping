using UnityEngine;
using UnityEngine.UI;

public class Demo_FloatController : MonoBehaviour
{
    [SerializeField]
    private FloatVariable m_FloatVariable;

    [SerializeField]
    private EventChannel m_FloatUpdateEventChannel;

    [SerializeField]
    private Button m_ActionButton;

    public void HandleButtonPressed()
    {
        m_FloatVariable.ApplyChange(1.0f);
        m_FloatUpdateEventChannel.Invoke();
    }
}
