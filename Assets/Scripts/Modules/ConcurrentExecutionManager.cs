
using UnityEngine;
using TMPro;

public class ConcurrentExecutionManager : MonoBehaviour
{
    [Header("3D References")]
    [SerializeField] private TMP_Text sharedCounterText;
    [SerializeField] private TMP_Text concurrentStatusText;

    [Header("Thread Status Lights")]
    [SerializeField] private Renderer threadAStatusLight;
    [SerializeField] private Renderer threadBStatusLight;

    private int sharedCounter = 0;
    private int currentStep = 0;

    private Material threadAMaterial;
    private Material threadBMaterial;

    private readonly Color idleColor =
        new Color32(148, 163, 184, 255);

    private readonly Color runningColor =
        new Color32(0, 255, 153, 255);

    private Module2CodeHighlighter codeHighlighter;    

    private ThreadExecutionAnimator threadAnimator;

    private void Awake()
    {

        threadAnimator = GetComponent<ThreadExecutionAnimator>();
        if (threadAStatusLight != null)
            threadAMaterial = threadAStatusLight.material;

        if (threadBStatusLight != null)
            threadBMaterial = threadBStatusLight.material;
    }

    private void Start()
    {
        codeHighlighter =
    FindFirstObjectByType<Module2CodeHighlighter>();
        ResetSimulation();
    }

   
public void NextStep()
{
    if (currentStep >= 4)
    {
        Debug.Log("Concurrent execution completed.");
        return;
    }

    // Automatic alternating execution
    bool isThreadA = currentStep % 2 == 0;

    ExecuteThread(isThreadA);
}

public void ExecuteThreadA()
{
    ExecuteThread(true);
}

public void ExecuteThreadB()
{
    ExecuteThread(false);
}

private void ExecuteThread(bool isThreadA)
{
    if (currentStep >= 4)
    {
        Debug.Log("Simulation completed. Press RESET.");
        return;
    }

    sharedCounter++;

    string activeThread = isThreadA ? "A" : "B";

    UpdateCounterDisplay();
    SetThreadLights(isThreadA);
    if (threadAnimator != null)
{
    if (isThreadA)
        threadAnimator.AnimateThreadA();
    else
        threadAnimator.AnimateThreadB();
}

    if (concurrentStatusText != null)
    {
        concurrentStatusText.text =
            "THREAD " + activeThread + ": RUNNING";
    }

    if (codeHighlighter == null)
{
    codeHighlighter =
        FindFirstObjectByType<Module2CodeHighlighter>();
}

if (codeHighlighter != null)
{
    if (isThreadA)
        codeHighlighter.HighlightThreadA(sharedCounter);
    else
        codeHighlighter.HighlightThreadB(sharedCounter);
}

    currentStep++;

    Debug.Log(
        "Thread " + activeThread +
        " executed. Counter = " + sharedCounter
    );
}


    public void ResetSimulation()
    {
        sharedCounter = 0;
        currentStep = 0;

        UpdateCounterDisplay();

        if (threadAnimator != null)
    threadAnimator.ResetAnimations();

        if (concurrentStatusText != null)
            concurrentStatusText.text = "A: READY | B: READY";

        if (threadAMaterial != null)
            threadAMaterial.color = idleColor;

        if (threadBMaterial != null)
            threadBMaterial.color = idleColor;

        Debug.Log("Module 2 simulation reset.");

        if (codeHighlighter == null)
{
    codeHighlighter =
        FindFirstObjectByType<Module2CodeHighlighter>();
}

if (codeHighlighter != null)
    codeHighlighter.ShowReady();
    }

    private void UpdateCounterDisplay()
    {
        if (sharedCounterText != null)
            sharedCounterText.text = "COUNTER: " + sharedCounter;
    }

    private void SetThreadLights(bool isThreadA)
    {
        if (threadAMaterial != null)
            threadAMaterial.color =
                isThreadA ? runningColor : idleColor;

        if (threadBMaterial != null)
            threadBMaterial.color =
                isThreadA ? idleColor : runningColor;
    }
}
