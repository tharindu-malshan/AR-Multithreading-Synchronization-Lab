
using UnityEngine;

public class SemaphoreVisualFeedback : MonoBehaviour
{
    [Header("Resource Renderers")]
    [SerializeField] private Renderer resource1Renderer;
    [SerializeField] private Renderer resource2Renderer;

    [Header("Resource Materials")]
    [SerializeField] private Material availableMaterial;
    [SerializeField] private Material threadAMaterial;
    [SerializeField] private Material threadBMaterial;
    [SerializeField] private Material threadCMaterial;

    public void ResetResources()
    {
        SetResource1Available();
        SetResource2Available();
    }

    public void SetResource1Available()
    {
        ChangeMaterial(resource1Renderer, availableMaterial);
    }

    public void SetResource2Available()
    {
        ChangeMaterial(resource2Renderer, availableMaterial);
    }

    public void SetResource1ThreadA()
    {
        ChangeMaterial(resource1Renderer, threadAMaterial);
    }

    public void SetResource2ThreadB()
    {
        ChangeMaterial(resource2Renderer, threadBMaterial);
    }

    public void SetResource1ThreadC()
    {
        ChangeMaterial(resource1Renderer, threadCMaterial);
    }

    private void ChangeMaterial(
        Renderer targetRenderer,
        Material newMaterial)
    {
        if (targetRenderer != null && newMaterial != null)
        {
            targetRenderer.sharedMaterial = newMaterial;
        }
    }
}
