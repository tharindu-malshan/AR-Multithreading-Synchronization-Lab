
using UnityEngine;
using TMPro;

public class MutexSimulationManager : MonoBehaviour
{
    [Header("Module 4 UI")]
    [SerializeField] private TMP_Text mutexStatusText;
    [SerializeField] private TMP_Text mutexLabel;
    [SerializeField] private TMP_Text sharedCounterText;

    private MutexVisualFeedback mutexVisual;
    private CriticalSectionVisualFeedback criticalVisual;
    private ThreadExecutionAnimator threadAnimator;

    private Module4CodeHighlighter codeHighlighter;

    private int sharedCounter = 0;
    private int currentStep = 0;

    private bool isLocked = false;
    private string lockOwner = "NONE";

    private string threadAState = "READY";
    private string threadBState = "READY";

    private void Awake()
    {
        mutexVisual = GetComponent<MutexVisualFeedback>();

        criticalVisual =
            GetComponent<CriticalSectionVisualFeedback>();

        threadAnimator = GetComponent<ThreadExecutionAnimator>();
    }

    private void Start()
    {
        ResetSimulation();
    }

    public void ResetSimulation()
    {
        sharedCounter = 0;
        currentStep = 0;

        isLocked = false;
        lockOwner = "NONE";

        threadAState = "READY";
        threadBState = "READY";

        if (mutexVisual != null)
            mutexVisual.ShowUnlocked();

        if (criticalVisual != null)
            criticalVisual.ShowIdle();

        if (threadAnimator != null)
            threadAnimator.ResetAnimations();

        UpdateDisplay("Simulation ready. Mutex is unlocked.");

        Module4CodeHighlighter highlighter = FindCodeHighlighter();

if (highlighter != null)
    highlighter.ShowReady();
    }

    public void NextStep()
    {
        if (currentStep >= 7)
            return;

        currentStep++;

        switch (currentStep)
        {
            case 1:
                isLocked = true;
                lockOwner = "THREAD A";
                threadAState = "RUNNING";

                if (mutexVisual != null)
                    mutexVisual.ShowLockedByA();

                if (criticalVisual != null)
                    criticalVisual.ShowThreadA();

                if (threadAnimator != null)
                    threadAnimator.AnimateThreadA();

                UpdateDisplay(
                    "Thread A acquired the mutex lock."
                );
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

                if (threadAnimator != null)
                    threadAnimator.AnimateThreadA();

                UpdateDisplay(
                    "Thread A updated the shared counter " +
                    "inside the critical section."
                );
                break;

            case 4:
                isLocked = false;
                lockOwner = "NONE";
                threadAState = "COMPLETED";

                if (mutexVisual != null)
                    mutexVisual.ShowUnlocked();

                if (criticalVisual != null)
                    criticalVisual.ShowIdle();

                UpdateDisplay(
                    "Thread A released the mutex lock."
                );
                break;

            case 5:
                isLocked = true;
                lockOwner = "THREAD B";
                threadBState = "RUNNING";

                if (mutexVisual != null)
                    mutexVisual.ShowLockedByB();

                if (criticalVisual != null)
                    criticalVisual.ShowThreadB();

                if (threadAnimator != null)
                    threadAnimator.AnimateThreadB();

                UpdateDisplay(
                    "Thread B acquired the mutex lock."
                );
                break;

            case 6:
                sharedCounter++;

                if (threadAnimator != null)
                    threadAnimator.AnimateThreadB();

                UpdateDisplay(
                    "Thread B updated the shared counter " +
                    "inside the critical section."
                );
                break;

            case 7:
                isLocked = false;
                lockOwner = "NONE";
                threadBState = "COMPLETED";

                if (mutexVisual != null)
                    mutexVisual.ShowUnlocked();

                if (criticalVisual != null)
                    criticalVisual.ShowIdle();

                UpdateDisplay(
                    "Thread B released the mutex. " +
                    "Both updates were saved successfully."
                );
                break;
        }

        Module4CodeHighlighter highlighter = FindCodeHighlighter();

if (highlighter != null)
{
    highlighter.HighlightStep(currentStep, sharedCounter);
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
            mutexLabel.text = "MUTEX\n" + mutexState;

        if (sharedCounterText != null)
            sharedCounterText.text = "COUNTER: " + sharedCounter;
    }

    private Module4CodeHighlighter FindCodeHighlighter()
{
    if (codeHighlighter == null)
    {
        codeHighlighter =
            FindFirstObjectByType<Module4CodeHighlighter>();
    }

    return codeHighlighter;
}
}
