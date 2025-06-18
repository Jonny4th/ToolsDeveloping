using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

[Serializable]
public class EventReference
{
    public EventChannel EventChannel;
    public AssetReference AssetReference;

    public void AddListener(Action listener)
    {
        if(EventChannel != null)
        {
            EventChannel.AddListener(listener);
        }
    }

    public void RemoveListener(Action listener)
    {
        if(EventChannel != null)
        {
            EventChannel.RemoveListener(listener);
        }
    }

    public void RemoveAllListeners()
    {
        if(EventChannel != null)
        {
            EventChannel.RemoveAllListeners();
        }
    }

    public async Task LoadAddressableAsset()
    {
        var handle = AssetReference.LoadAssetAsync<EventChannel>();
        while(!handle.IsDone) await Task.Delay(100);
        EventChannel = handle.Result;
        return;
    }

    public void Release()
    {
        AssetReference.ReleaseAsset();
    }
}
