
using UnityEngine;

public class Module5UIController : MonoBehaviour
{
    private SemaphoreSimulationManager simulationManager;

    private SemaphoreSimulationManager FindManager()
    {
        if (simulationManager != null &&
            simulationManager.gameObject.activeInHierarchy)
        {
            return simulationManager;
        }

        simulationManager = null;

        SemaphoreSimulationManager[] managers =
            FindObjectsByType<SemaphoreSimulationManager>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );

        foreach (SemaphoreSimulationManager manager in managers)
        {
            if (manager.gameObject.name.Contains("(Clone)"))
            {
                simulationManager = manager;
                break;
            }
        }

        return simulationManager;
    }

    public void StartSimulation()
    {
        SemaphoreSimulationManager manager = FindManager();

        if (manager != null)
            manager.ResetSimulation();
        else
            Debug.LogWarning("Place the AR Lab first.");
    }

    public void NextStep()
    {
        SemaphoreSimulationManager manager = FindManager();

        if (manager != null)
            manager.NextStep();
        else
            Debug.LogWarning("Place the AR Lab first.");
    }

    public void ResetSimulation()
    {
        SemaphoreSimulationManager manager = FindManager();

        if (manager != null)
            manager.ResetSimulation();
        else
            Debug.LogWarning("Place the AR Lab first.");
    }
}
