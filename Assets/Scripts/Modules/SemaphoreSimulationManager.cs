
using UnityEngine;
using TMPro;

public class SemaphoreSimulationManager : MonoBehaviour
{
    [Header("Semaphore UI")]
    [SerializeField] private TMP_Text semaphoreStatusText;
    [SerializeField] private TMP_Text semaphoreCountLabel;

    private SemaphoreVisualFeedback visualFeedback;

    private SemaphoreThreadAnimator threadAnimator;

    private Module5CodeHighlighter codeHighlighter;

    private const int MaxPermits = 2;

    private int availablePermits = MaxPermits;
    private int currentStep = 0;

    private string threadAState = "READY";
    private string threadBState = "READY";
    private string threadCState = "READY";

    private string resource1Owner = "AVAILABLE";
    private string resource2Owner = "AVAILABLE";

    private void Awake()
    {
        visualFeedback = GetComponent<SemaphoreVisualFeedback>();
        threadAnimator = GetComponent<SemaphoreThreadAnimator>();
    }

    private void Start()
    {
        ResetSimulation();
    }

    public void ResetSimulation()
    {
        currentStep = 0;
        availablePermits = MaxPermits;

        threadAState = "READY";
        threadBState = "READY";
        threadCState = "READY";

        resource1Owner = "AVAILABLE";
        resource2Owner = "AVAILABLE";

        if (visualFeedback != null)
            visualFeedback.ResetResources();

            if (threadAnimator != null)
    threadAnimator.ResetAnimations();

        UpdateDisplay(
            "Simulation ready. Two permits are available."
        );

        Module5CodeHighlighter highlighter = FindCodeHighlighter();

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
                availablePermits--;
                threadAState = "RUNNING";
                resource1Owner = "THREAD A";

                if (visualFeedback != null)
                    visualFeedback.SetResource1ThreadA();

                UpdateDisplay(
                    "Thread A acquired Resource 1."
                );
                if (threadAnimator != null)
    threadAnimator.AnimateThreadA();
                break;

            case 2:
                availablePermits--;
                threadBState = "RUNNING";
                resource2Owner = "THREAD B";

                if (visualFeedback != null)
                    visualFeedback.SetResource2ThreadB();

                UpdateDisplay(
                    "Thread B acquired Resource 2."
                );
                if (threadAnimator != null)
    threadAnimator.AnimateThreadB();
                break;

            case 3:
                threadCState = "WAITING";

                UpdateDisplay(
                    "Thread C is waiting. No permits available."
                );
                if (threadAnimator != null)
    threadAnimator.ShowThreadCWaiting();
                break;

            case 4:
                availablePermits++;
                threadAState = "COMPLETED";
                resource1Owner = "AVAILABLE";

                if (visualFeedback != null)
                    visualFeedback.SetResource1Available();

                UpdateDisplay(
                    "Thread A released Resource 1."
                );
                break;

            case 5:
                availablePermits--;
                threadCState = "RUNNING";
                resource1Owner = "THREAD C";

                if (visualFeedback != null)
                    visualFeedback.SetResource1ThreadC();

                UpdateDisplay(
                    "Thread C acquired Resource 1."
                );
                if (threadAnimator != null)
{
    threadAnimator.HideThreadCWaiting();
    threadAnimator.AnimateThreadC();
}
                break;

            case 6:
                availablePermits++;
                threadBState = "COMPLETED";
                resource2Owner = "AVAILABLE";

                if (visualFeedback != null)
                    visualFeedback.SetResource2Available();

                UpdateDisplay(
                    "Thread B released Resource 2."
                );
                break;

            case 7:
                availablePermits++;
                threadCState = "COMPLETED";
                resource1Owner = "AVAILABLE";

                if (visualFeedback != null)
                    visualFeedback.SetResource1Available();

                UpdateDisplay(
                    "All threads completed. Both permits are free."
                );
                if (threadAnimator != null)
    threadAnimator.HideThreadCWaiting();
                break;
        }
        Module5CodeHighlighter highlighter = FindCodeHighlighter();

if (highlighter != null)
{
    highlighter.HighlightStep(currentStep, availablePermits);
}
    }

    private void UpdateDisplay(string message)
    {
        if (semaphoreStatusText != null)
        {
            semaphoreStatusText.text =
                "SEMAPHORE & RESOURCE MANAGEMENT\n\n" +
                "AVAILABLE PERMITS: " + availablePermits +
                " / " + MaxPermits + "\n\n" +
                "THREAD A: " + threadAState + "\n" +
                "THREAD B: " + threadBState + "\n" +
                "THREAD C: " + threadCState + "\n\n" +
                "RESOURCE 1: " + resource1Owner + "\n" +
                "RESOURCE 2: " + resource2Owner + "\n\n" +
                message;
        }

        if (semaphoreCountLabel != null)
        {
            semaphoreCountLabel.text =
                "SEMAPHORE\nPERMITS: " + availablePermits;
        }
    }

    private Module5CodeHighlighter FindCodeHighlighter()
{
    if (codeHighlighter == null)
    {
        codeHighlighter =
            FindFirstObjectByType<Module5CodeHighlighter>();
    }

    return codeHighlighter;
}
}
