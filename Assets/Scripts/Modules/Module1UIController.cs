
using UnityEngine;
using TMPro;

public class Module1UIController : MonoBehaviour
{
    [Header("Optional Status Text")]
    [SerializeField] private TMP_Text statusText;

    private ThreadSimulationManager simulation;

    private bool FindSimulation()
    {
        if (simulation != null)
            return true;

        ThreadSimulationManager[] managers =
            FindObjectsByType<ThreadSimulationManager>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );

        if (managers.Length > 0)
        {
            simulation = managers[0];
            return true;
        }

        if (statusText != null)
            statusText.text = "Place the AR lab first.";

        Debug.LogWarning("Place the AR lab before using controls.");
        return false;
    }

    public void StartThread()
    {
        if (!FindSimulation()) return;

        simulation.ResetSimulation();
        simulation.NextStep();

        UpdateStatus("Thread A: READY");
    }

    public void NextStep()
    {
        if (!FindSimulation()) return;

        simulation.NextStep();
        UpdateStatus("Next lifecycle step executed.");
    }

    public void ResetThread()
    {
        if (!FindSimulation()) return;

        simulation.ResetSimulation();
        UpdateStatus("Thread A: NEW");
    }

    private void UpdateStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;
    }
}
