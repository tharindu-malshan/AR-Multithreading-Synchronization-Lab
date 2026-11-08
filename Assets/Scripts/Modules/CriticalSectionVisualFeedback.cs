
using UnityEngine;

public class CriticalSectionVisualFeedback : MonoBehaviour
{
    [Header("Critical Section Renderer")]
    [SerializeField] private Renderer criticalSectionRenderer;

    [Header("Materials")]
    [SerializeField] private Material idleMaterial;
    [SerializeField] private Material threadAMaterial;
    [SerializeField] private Material threadBMaterial;

    public void ShowIdle()
    {
        ChangeMaterial(idleMaterial);
    }

    public void ShowThreadA()
    {
        ChangeMaterial(threadAMaterial);
    }

    public void ShowThreadB()
    {
        ChangeMaterial(threadBMaterial);
    }

    private void ChangeMaterial(Material material)
    {
        if (criticalSectionRenderer != null && material != null)
        {
            criticalSectionRenderer.sharedMaterial = material;
        }
    }
}
