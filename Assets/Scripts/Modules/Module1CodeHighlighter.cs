
using UnityEngine;
using TMPro;

public class Module1CodeHighlighter : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text codeDisplayText;
    [SerializeField] private TMP_Text explanationText;

    private readonly string[] codeLines =
    {
        "Thread worker = new Thread(Work);",
        "// Thread is ready to start",
        "worker.Start();",
        "// Worker is executing",
        "// Worker has completed"
    };

    private void Start()
    {
        ShowState(ThreadSimulationManager.ThreadState.New);
    }

    public void ShowState(ThreadSimulationManager.ThreadState state)
    {
        int activeLine = 0;
        string explanation = "";

        switch (state)
        {
            case ThreadSimulationManager.ThreadState.New:
                activeLine = 0;
                explanation = "A new thread object has been created.";
                break;

            case ThreadSimulationManager.ThreadState.Ready:
                activeLine = 1;
                explanation = "The thread is ready to run.";
                break;

            case ThreadSimulationManager.ThreadState.Running:
                activeLine = 2;
                explanation = "The thread begins executing its task.";
                break;

            case ThreadSimulationManager.ThreadState.Waiting:
                activeLine = 3;
                explanation = "The thread is waiting for a resource.";
                break;

            case ThreadSimulationManager.ThreadState.Terminated:
                activeLine = 4;
                explanation = "The thread has completed execution.";
                break;
        }

        string result = "";

        for (int i = 0; i < codeLines.Length; i++)
        {
            string line = (i + 1).ToString("00") + "  " + codeLines[i];

            if (i == activeLine)
            {
                result += "<color=#66FFD1><b>▶ " +
                          line + "</b></color>\n";
            }
            else
            {
                result += "<color=#AFC3D8>   " +
                          line + "</color>\n";
            }
        }

        if (codeDisplayText != null)
            codeDisplayText.text = result;

        if (explanationText != null)
            explanationText.text = explanation;
    }
}
