
using UnityEngine;

public class RaceConditionVisualFeedback : MonoBehaviour
{
    [Header("Indicator")]
    [SerializeField] private Renderer resultIndicator;

    [Header("Materials")]
    [SerializeField] private Material readyMaterial;
    [SerializeField] private Material successMaterial;
    [SerializeField] private Material warningMaterial;

    public void ShowReady()
    {
        ApplyMaterial(readyMaterial);
    }

    public void ShowSuccess()
    {
        ApplyMaterial(successMaterial);
    }

    public void ShowWarning()
    {
        ApplyMaterial(warningMaterial);
    }

    private void ApplyMaterial(Material material)
    {
        if (resultIndicator == null || material == null)
            return;

        resultIndicator.sharedMaterial = material;
    }
}
