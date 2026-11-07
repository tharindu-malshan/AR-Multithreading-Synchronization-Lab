
using UnityEngine;

public class Module3UIController : MonoBehaviour
{
    private RaceConditionSimulationManager manager;

    private RaceConditionSimulationManager GetManager()
    {
        if (manager == null)
        {
            RaceConditionSimulationManager[] managers =
                FindObjectsByType<RaceConditionSimulationManager>(
                    FindObjectsInactive.Exclude,
                    FindObjectsSortMode.None
                );

            foreach (RaceConditionSimulationManager item in managers)
            {
                if (item.gameObject.name.Contains("(Clone)"))
                {
                    manager = item;
                    break;
                }
            }
        }

        return manager;
    }

    public void StartSimulation()
    {
        RaceConditionSimulationManager sim = GetManager();

        if (sim != null)
            sim.ResetSimulation();
        else
            Debug.LogWarning("Place the AR Lab first!");
    }

    public void NextStep()
    {
        RaceConditionSimulationManager sim = GetManager();

        if (sim != null)
            sim.NextStep();
        else
            Debug.LogWarning("Place the AR Lab first!");
    }

    public void SetNormalMode()
    {
        RaceConditionSimulationManager sim = GetManager();

        if (sim != null)
            sim.SetNormalMode();
        else
            Debug.LogWarning("Place the AR Lab first!");
    }

    public void SetRaceConditionMode()
    {
        RaceConditionSimulationManager sim = GetManager();

        if (sim != null)
            sim.SetRaceConditionMode();
        else
            Debug.LogWarning("Place the AR Lab first!");
    }

    public void ResetSimulation()
    {
        RaceConditionSimulationManager sim = GetManager();

        if (sim != null)
            sim.ResetSimulation();
        else
            Debug.LogWarning("Place the AR Lab first!");
    }
}
