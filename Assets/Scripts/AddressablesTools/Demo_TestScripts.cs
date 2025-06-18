using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Demo_TestScripts : MonoBehaviour
{
    [Header("Scriptable References")]
    [SerializeField]
    private FloatReference m_FloatReference;

    [SerializeField]
    private EventReference m_FloatUpdateEventReference;

    [Header("UIs")]
    [SerializeField]
    private TextMeshProUGUI m_ResultDisplay;

    [SerializeField]
    private Slider m_ProgressBar;

    private AsyncOperationHandle m_SceneHandle;
    private Coroutine m_Progress;

    public async void LoadGame()
    {
        await LoadScriptableAssets();
        m_SceneHandle = Addressables.LoadSceneAsync("GameScene01", LoadSceneMode.Additive);
        m_SceneHandle.Completed += HandleOnSceneLoadComplete;
        m_Progress = StartCoroutine(LoadingProgress());
    }

    private IEnumerator LoadingProgress()
    {
        while(!m_SceneHandle.IsDone)
        {
            m_ProgressBar.value = m_SceneHandle.PercentComplete;
            Debug.Log(m_SceneHandle.PercentComplete);
            yield return null;
        }
    }

    private void HandleOnSceneLoadComplete(AsyncOperationHandle handle)
    {
        m_ResultDisplay.gameObject.SetActive(true);
        m_ProgressBar.gameObject.SetActive(false);
    }

    private async Task LoadScriptableAssets()
    {
        await m_FloatReference.LoadAddressableAsset();
        m_FloatReference.Variable.SetValue(0);

        await m_FloatUpdateEventReference.LoadAddressableAsset();
        m_FloatUpdateEventReference.AddListener(HandleFloatUpdateEvent);
    }

    private void HandleFloatUpdateEvent()
    {
        m_ResultDisplay.text = $"{m_FloatReference.Value}";
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        m_FloatUpdateEventReference.RemoveListener(HandleFloatUpdateEvent);
        Addressables.UnloadSceneAsync(m_SceneHandle);
    }
}
