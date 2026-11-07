
using UnityEngine;
using TMPro;

public class MutexSimulationManager : MonoBehaviour
{
    [Header("Module 4 UI")]
    [SerializeField] private TMP_Text mutexStatusText;
    [SerializeField] private TMP_Text mutexLabel;
    [SerializeField] private TMP_Text sharedCounterText;

    private int sharedCounter = 0;
    private int currentStep = 0;

    private bool isLocked = false;
    private string lockOwner = "NONE";

    private string threadAState = "READY";
    private string threadBState = "READY";

    private MutexVisualFeedback visualFeedback;

    private void Start()
    {
        ResetSimulation();
    }

    private void Awake()
{
    visualFeedback = GetComponent<MutexVisualFeedback>();
}

    public void ResetSimulation()
    {
        sharedCounter = 0;
        currentStep = 0;

        isLocked = false;
        lockOwner = "NONE";

        threadAState = "READY";
        threadBState = "READY";

        if (visualFeedback != null)
    visualFeedback.ShowUnlocked();

        UpdateDisplay("Simulation ready. Mutex is unlocked.");
    }

    public void NextStep()
    {
        if (currentStep >= 7)
        {
            UpdateDisplay("Simulation completed. Final counter = 2.");
            return;
        }

        currentStep++;

        switch (currentStep)
        {
            case 1:
                isLocked = true;
                lockOwner = "THREAD A";
                threadAState = "RUNNING";

                UpdateDisplay(
                    "Thread A acquired the mutex lock."
                );

                if (visualFeedback != null)
                    visualFeedback.ShowLockedByA();  
                break;

            case 2:
                threadBState = "WAITING";

                UpdateDisplay(
                    "Thread B tried to acquire the mutex, " +
                    "but Thread A owns it. Thread B must wait."
                );
                break;

            case 3:
                sharedCounter++;
                threadAState = "RUNNING";

                UpdateDisplay(
                    "Thread A entered the critical section " +
                    "and updated the shared counter."
                );
                break;

            case 4:
                isLocked = false;
                lockOwner = "NONE";
                threadAState = "COMPLETED";

                UpdateDisplay(
                    "Thread A released the mutex lock."
                );

                if (visualFeedback != null)
                visualFeedback.ShowUnlocked();
                break;

            case 5:
                isLocked = true;
                lockOwner = "THREAD B";
                threadBState = "RUNNING";

                UpdateDisplay(
                    "Thread B acquired the mutex lock."
                );

                if (visualFeedback != null)
                    visualFeedback.ShowLockedByB();
                break;

            case 6:
                sharedCounter++;
                threadBState = "RUNNING";

                UpdateDisplay(
                    "Thread B entered the critical section " +
                    "and updated the shared counter."
                );
                break;

            case 7:
                isLocked = false;
                lockOwner = "NONE";
                threadBState = "COMPLETED";

                UpdateDisplay(
                    "Thread B released the mutex. " +
                    "Both updates were saved successfully."
                );
                if (visualFeedback != null)
    visualFeedback.ShowUnlocked();
                break;
        }
    }

    private void UpdateDisplay(string message)
    {
        string mutexState = isLocked
            ? "LOCKED BY " + lockOwner
            : "UNLOCKED";

        if (mutexStatusText != null)
        {
            mutexStatusText.text =
                "MUTEX & CRITICAL SECTION\n\n" +
                "MUTEX: " + mutexState + "\n" +
                "THREAD A: " + threadAState + "\n" +
                "THREAD B: " + threadBState + "\n" +
                "COUNTER: " + sharedCounter + "\n\n" +
                message;
        }

        if (mutexLabel != null)
        {
            mutexLabel.text =
                "MUTEX\n" + mutexState;
        }

        if (sharedCounterText != null)
        {
            sharedCounterText.text =
                "COUNTER: " + sharedCounter;
        }
    }
}
