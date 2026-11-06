
using UnityEngine;

public class Module2UIController : MonoBehaviour
{
    private ConcurrentExecutionManager simulation;

    private bool FindSimulation()
    {
        if (simulation != null)
            return true;

        ConcurrentExecutionManager[] managers =
            FindObjectsByType<ConcurrentExecutionManager>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );

        foreach (ConcurrentExecutionManager manager in managers)
        {
            if (manager.gameObject.name == "MultithreadingLab(Clone)")
            {
                simulation = manager;
                return true;
            }
        }

        Debug.LogWarning("Place the AR lab first.");
        return false;
    }

    public void StartSimulation()
    {
        if (!FindSimulation()) return;

        simulation.ResetSimulation();

        Debug.Log("Module 2 simulation started.");
    }

    public void NextStep()
    {
        if (!FindSimulation()) return;

        simulation.NextStep();
    }

    public void ResetSimulation()
    {
        if (!FindSimulation()) return;

        simulation.ResetSimulation();
    }

    
public void RunThreadA()
{
    if (!FindSimulation()) return;

    simulation.ExecuteThreadA();
}

public void RunThreadB()
{
    if (!FindSimulation()) return;

    simulation.ExecuteThreadB();
}

}
