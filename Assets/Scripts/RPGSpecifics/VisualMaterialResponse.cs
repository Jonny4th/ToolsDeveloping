using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualMaterialResponse : MonoBehaviour
{
    public Material respondingMaterial;
    public bool isBlinkingResponse;
    public float BlinkLength;

    private Material originalMaterial;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    public void Respond()
    {
        meshRenderer.material = respondingMaterial;
        if(isBlinkingResponse)
        {
            StartCoroutine(ReturnToOriginal());
        }
    }

    IEnumerator ReturnToOriginal()
    {
        yield return new WaitForSeconds(BlinkLength);
        meshRenderer.material = originalMaterial;
        yield return null;
    }
}
