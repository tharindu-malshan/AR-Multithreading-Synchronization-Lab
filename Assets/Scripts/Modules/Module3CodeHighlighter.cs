
using UnityEngine;
using TMPro;

public class Module3CodeHighlighter : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text codeDisplayText;
    [SerializeField] private TMP_Text explanationText;

    private const string DefaultColor = "#E0E6F0";
    private const string HighlightColor = "#65F5B5";

    private string[] codeLines =
    {
        "int counter = 0;",
        "",
        "void Increment()",
        "{",
        "    int local = counter;",
        "    local = local + 1;",
        "    counter = local;",
        "}"
    };

    public void ShowReady(bool raceMode)
    {
        UpdateCode(-1);

        if (explanationText != null)
        {
            explanationText.text = raceMode
                ? "RACE CONDITION MODE\n" +
                  "Both threads may read the same old value. " +
                  "Press NEXT STEP to see how an update is lost."
                : "NORMAL MODE\n" +
                  "Thread A completes before Thread B starts. " +
                  "Press NEXT STEP to observe the correct result.";
        }
    }

    public void HighlightStep(
        int step,
        bool raceMode,
        int sharedCounter)
    {
        int lineIndex = -1;
        string threadName = "";
        string operation = "";

        if (raceMode)
        {
            switch (step)
            {
                case 1:
                    lineIndex = 4;
                    threadName = "Thread A";
                    operation = "READ";
                    break;

                case 2:
                    lineIndex = 4;
                    threadName = "Thread B";
                    operation = "READ";
                    break;

                case 3:
                    lineIndex = 5;
                    threadName = "Thread A";
                    operation = "ADD";
                    break;

                case 4:
                    lineIndex = 5;
                    threadName = "Thread B";
                    operation = "ADD";
                    break;

                case 5:
                    lineIndex = 6;
                    threadName = "Thread A";
                    operation = "WRITE";
                    break;

                case 6:
                    lineIndex = 6;
                    threadName = "Thread B";
                    operation = "WRITE";
                    break;
            }
        }
        else
        {
            switch (step)
            {
                case 1:
                    lineIndex = 4;
                    threadName = "Thread A";
                    operation = "READ";
                    break;

                case 2:
                    lineIndex = 5;
                    threadName = "Thread A";
                    operation = "ADD";
                    break;

                case 3:
                    lineIndex = 6;
                    threadName = "Thread A";
                    operation = "WRITE";
                    break;

                case 4:
                    lineIndex = 4;
                    threadName = "Thread B";
                    operation = "READ";
                    break;

                case 5:
                    lineIndex = 5;
                    threadName = "Thread B";
                    operation = "ADD";
                    break;

                case 6:
                    lineIndex = 6;
                    threadName = "Thread B";
                    operation = "WRITE";
                    break;
            }
        }

        UpdateCode(lineIndex);

        if (explanationText == null)
            return;

        string explanation = "";

        switch (operation)
        {
            case "READ":
                explanation =
                    "reads the current shared counter " +
                    "into its local variable.";
                break;

            case "ADD":
                explanation =
                    "adds 1 to its local value. " +
                    "Shared memory has not changed yet.";
                break;

            case "WRITE":
                explanation =
                    "writes its local value back " +
                    "to the shared counter.";
                break;
        }

        explanationText.text =
            threadName + " - " + operation + "\n" +
            threadName + " " + explanation + "\n" +
            "Shared Counter: " + sharedCounter;

        if (step == 6)
        {
            if (raceMode)
            {
                explanationText.text +=
                    "\n\nLOST UPDATE!\n" +
                    "Expected: 2 | Actual: 1\n" +
                    "Both threads read the old value 0. " +
                    "One increment was overwritten.";
            }
            else
            {
                explanationText.text +=
                    "\n\nCORRECT RESULT!\n" +
                    "Expected: 2 | Actual: 2\n" +
                    "Both increments were saved.";
            }
        }
    }

    private void UpdateCode(int highlightedLine)
    {
        if (codeDisplayText == null)
            return;

        string result = "";

        for (int i = 0; i < codeLines.Length; i++)
        {
            string color = i == highlightedLine
                ? HighlightColor
                : DefaultColor;

            result +=
                "<color=" + color + ">" +
                codeLines[i] +
                "</color>";

            if (i < codeLines.Length - 1)
                result += "\n";
        }

        codeDisplayText.text = result;
    }
}
