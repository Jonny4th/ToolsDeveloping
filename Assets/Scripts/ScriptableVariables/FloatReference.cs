// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// Modified: Kajornpop Toboonchuay
// Date:   18/06/25

using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

[Serializable] 
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;
    public AssetReference AssetReference;

    public FloatReference()
    { }

    public FloatReference(float value)
    {
        UseConstant = true;
        ConstantValue = value;
    }
    
    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public async Task LoadAddressableAsset()
    {
        var handle = AssetReference.LoadAssetAsync<FloatVariable>();
        while(!handle.IsDone) await Task.Delay(100);
        Variable = handle.Result;
        return;
    }

    public void Release()
    {
        AssetReference.ReleaseAsset();
    }

    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }
}
