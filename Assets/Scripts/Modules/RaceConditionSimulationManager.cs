
using UnityEngine;
using TMPro;

public class RaceConditionSimulationManager : MonoBehaviour
{
    public enum SimulationMode
    {
        Normal,
        RaceCondition
    }

    private Module3CodeHighlighter codeHighlighter;
    private ThreadExecutionAnimator threadAnimator;
    private RaceConditionVisualFeedback visualFeedback;

    [Header("3D Text References")]
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text threadALocalText;
    [SerializeField] private TMP_Text threadBLocalText;
    [SerializeField] private TMP_Text sharedCounterText;

    [Header("Simulation Settings")]
    [SerializeField] private SimulationMode mode =
        SimulationMode.RaceCondition;

    private int sharedCounter;
    private int threadAValue;
    private int threadBValue;
    private bool threadAHasRead;
    private bool threadBHasRead;
    private int currentStep;

   private void Awake()
{
    threadAnimator = GetComponent<ThreadExecutionAnimator>();
    visualFeedback = GetComponent<RaceConditionVisualFeedback>();
}

    private void Start()
    {
        ResetSimulation();
    }

    public void SetNormalMode()
    {
        mode = SimulationMode.Normal;
        ResetSimulation();
    }

    public void SetRaceConditionMode()
    {
        mode = SimulationMode.RaceCondition;
        ResetSimulation();
    }

    public void NextStep()
    {
        if (currentStep >= 6)
        {
            Debug.Log("Simulation finished. Press RESET.");
            return;
        }

        if (mode == SimulationMode.Normal)
            ExecuteNormalStep();
        else
            ExecuteRaceConditionStep();

        currentStep++;
        UpdateDisplay();

        AnimateCurrentStep();

Module3CodeHighlighter highlighter = GetCodeHighlighter();

if (highlighter != null)
{
    highlighter.HighlightStep(
        currentStep,
        mode == SimulationMode.RaceCondition,
        sharedCounter
    );
}

if (currentStep == 6 && visualFeedback != null)
{
    if (mode == SimulationMode.Normal)
        visualFeedback.ShowSuccess();
    else
        visualFeedback.ShowWarning();
}
    }

    private void AnimateCurrentStep()
{
    if (threadAnimator == null)
        return;

    bool isThreadA;

    if (mode == SimulationMode.Normal)
    {
        // Normal: A completes first, then B.
        isThreadA = currentStep <= 3;
    }
    else
    {
        // Race: A and B operations alternate.
        isThreadA = currentStep % 2 == 1;
    }

    if (isThreadA)
        threadAnimator.AnimateThreadA();
    else
        threadAnimator.AnimateThreadB();
}

    private void ExecuteNormalStep()
    {
        switch (currentStep)
        {
            case 0:
                threadAValue = sharedCounter;
                threadAHasRead = true;
                break;

            case 1:
                threadAValue++;
                break;

            case 2:
                sharedCounter = threadAValue;
                break;

            case 3:
                threadBValue = sharedCounter;
                threadBHasRead = true;
                break;

            case 4:
                threadBValue++;
                break;

            case 5:
                sharedCounter = threadBValue;
                break;
        }
    }

    private void ExecuteRaceConditionStep()
    {
        switch (currentStep)
        {
            case 0:
                threadAValue = sharedCounter;
                threadAHasRead = true;
                break;

            case 1:
                threadBValue = sharedCounter;
                threadBHasRead = true;
                break;

            case 2:
                threadAValue++;
                break;

            case 3:
                threadBValue++;
                break;

            case 4:
                sharedCounter = threadAValue;
                break;

            case 5:
                sharedCounter = threadBValue;
                break;
        }
    }

    public void ResetSimulation()
    {
        sharedCounter = 0;
        threadAValue = 0;
        threadBValue = 0;
        threadAHasRead = false;
        threadBHasRead = false;
        currentStep = 0;

        UpdateDisplay();

        Module3CodeHighlighter highlighter = GetCodeHighlighter();

if (highlighter != null)
{
    highlighter.ShowReady(
        mode == SimulationMode.RaceCondition
    );
}

     if (threadAnimator != null)
{
    threadAnimator.ResetAnimations();
}

if (visualFeedback != null)
{
    visualFeedback.ShowReady();
}
    }

    private void UpdateDisplay()
    {
        if (sharedCounterText != null)
            sharedCounterText.text =
                "COUNTER: " + sharedCounter;

        if (threadALocalText != null)
            threadALocalText.text =
                "A LOCAL: " +
                (threadAHasRead ? threadAValue.ToString() : "-");

        if (threadBLocalText != null)
            threadBLocalText.text =
                "B LOCAL: " +
                (threadBHasRead ? threadBValue.ToString() : "-");

        if (statusText != null)
        {
            string message = GetStatusMessage();

            statusText.text =
                "RACE CONDITION LAB\n" +
                "MODE: " + mode + "\n" +
                "STATUS: " + message;
        }
    }

    private string GetStatusMessage()
    {
        if (currentStep == 0)
            return "READY";

        if (mode == SimulationMode.Normal)
        {
            switch (currentStep)
            {
                case 1: return "A READ";
                case 2: return "A CALCULATED";
                case 3: return "A WROTE";
                case 4: return "B READ";
                case 5: return "B CALCULATED";
                case 6: return "CORRECT RESULT: 2";
            }
        }
        else
        {
            switch (currentStep)
            {
                case 1: return "A READ";
                case 2: return "B READ";
                case 3: return "A CALCULATED";
                case 4: return "B CALCULATED";
                case 5: return "A WROTE";
                case 6: return "LOST UPDATE! EXPECTED 2";
            }
        }

        return "READY";
    }

    private Module3CodeHighlighter GetCodeHighlighter()
{
    if (codeHighlighter == null)
    {
        codeHighlighter =
            FindFirstObjectByType<Module3CodeHighlighter>();
    }

    return codeHighlighter;
}
}
