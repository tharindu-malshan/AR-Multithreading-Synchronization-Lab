
using UnityEngine;

public class MutexVisualFeedback : MonoBehaviour
{
    [Header("Mutex Renderer")]
    [SerializeField] private Renderer mutexRenderer;

    [Header("Mutex Materials")]
    [SerializeField] private Material unlockedMaterial;
    [SerializeField] private Material threadAMaterial;
    [SerializeField] private Material threadBMaterial;

    public void ShowUnlocked()
    {
        ChangeMaterial(unlockedMaterial);
    }

    public void ShowLockedByA()
    {
        ChangeMaterial(threadAMaterial);
    }

    public void ShowLockedByB()
    {
        ChangeMaterial(threadBMaterial);
    }

    private void ChangeMaterial(Material newMaterial)
    {
        if (mutexRenderer != null && newMaterial != null)
        {
            mutexRenderer.sharedMaterial = newMaterial;
        }
    }
}
