using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BootStrapper : MonoBehaviour
{
    [SerializeField]
    private int ActualGameSceneIndex;

    [SerializeField]
    private Demo_TestScripts m_GameManager;

    List<AsyncOperationHandle> m_AsyncOperations = new();
    private async void Awake()
    {
        await LoadAssetsWithLabel<Object>("Fundamentals");
        await LoadAssetsWithLabel<ScriptableObject>("ScriptableReference");
        LoadGame();
    }

    public async Task<IList<T>> LoadAssetsWithLabel<T>(string label)
    {
        var result = Addressables.LoadAssetsAsync<T>(label, null);
        m_AsyncOperations.Add(result);
        while(!result.IsDone) await Task.Delay(100);
        Debug.Log($"Load {label} done.");
        return result.Result;
    }

    public void LoadGame()
    {
        m_GameManager.LoadGame();
    }

    private void OnDestroy()
    {
        foreach(var operation in m_AsyncOperations)
        {
            Addressables.Release(operation);
        }

        m_AsyncOperations.Clear();
    }
}
