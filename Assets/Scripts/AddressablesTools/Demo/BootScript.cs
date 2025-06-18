using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BootScript : MonoBehaviour
{
    [SerializeField]
    private AssetReference m_DemoTestScriptsPrefab;

    [SerializeField]
    private GameObject m_botton;

    public void Load()
    {
        LoadAddressablesAssetsByLabel<UnityEngine.Object>("Fundamentals", (gbs) =>
        {
            LoadAddressablesAssetsByLabel<ScriptableObject>("ScriptableReference", (scriptables) =>
            {
                Addressables.InstantiateAsync(m_DemoTestScriptsPrefab).Completed += handle =>
                {
                    if(handle.Status != AsyncOperationStatus.Succeeded)
                    {
                        Debug.LogError("Failed to instantiate Demo_TestScripts.");
                        return;
                    }
                    
                    Debug.Log("Demo_TestScripts instantiated successfully.");
                    m_botton.SetActive(false);
                };
            });
        });
    }

    private void LoadAddressablesAssetsByLabel<T>(string label, Action<AsyncOperationHandle<IList<T>>> callback)
    {
        var handle = Addressables.LoadAssetsAsync<T>(label, null);
        handle.Completed += callback;
    }
}
