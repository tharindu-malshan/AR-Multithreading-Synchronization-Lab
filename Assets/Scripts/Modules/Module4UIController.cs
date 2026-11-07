
using UnityEngine;

public class Module4UIController : MonoBehaviour
{
    private MutexSimulationManager mutexManager;

    private MutexSimulationManager FindManager()
    {
        if (mutexManager != null &&
            mutexManager.gameObject.activeInHierarchy)
        {
            return mutexManager;
        }

        MutexSimulationManager[] managers =
            FindObjectsByType<MutexSimulationManager>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );

        foreach (MutexSimulationManager manager in managers)
        {
            if (manager.gameObject.name.Contains("(Clone)"))
            {
                mutexManager = manager;
                return mutexManager;
            }
        }

        mutexManager = null;
        return null;
    }

    public void StartSimulation()
    {
        MutexSimulationManager manager = FindManager();

        if (manager != null)
        {
            manager.ResetSimulation();
            Debug.Log("Module 4: Mutex simulation started.");
        }
        else
        {
            Debug.LogWarning(
                "Module 4: Place the AR lab first."
            );
        }
    }

    public void NextStep()
    {
        MutexSimulationManager manager = FindManager();

        if (manager != null)
        {
            manager.NextStep();
        }
        else
        {
            Debug.LogWarning(
                "Module 4: Place the AR lab first."
            );
        }
    }

    public void ResetSimulation()
    {
        MutexSimulationManager manager = FindManager();

        if (manager != null)
        {
            manager.ResetSimulation();
        }
        else
        {
            Debug.LogWarning(
                "Module 4: Place the AR lab first."
            );
        }
    }
}
