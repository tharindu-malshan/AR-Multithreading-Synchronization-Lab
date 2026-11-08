
using UnityEngine;
using TMPro;

public class Module4CodeHighlighter : MonoBehaviour
{
    [Header("Code Panel References")]
    [SerializeField] private TMP_Text codeDisplayText;
    [SerializeField] private TMP_Text explanationText;

    private readonly string[] codeLines =
    {
        "private readonly object mutex = new object();",
        "int counter = 0;",
        "",
        "void Increment()",
        "{",
        "    lock (mutex)",
        "    {",
        "        counter++;",
        "    }",
        "}"
    };

    private void OnEnable()
    {
        ShowReady();
    }

    public void ShowReady()
    {
        UpdateCode(-1);

        if (explanationText != null)
        {
            explanationText.text =
                "MUTEX & CRITICAL SECTION\n\n" +
                "A mutex protects shared data.\n" +
                "Only one thread can enter the critical section " +
                "at a time.\n\n" +
                "Press START to begin.";
        }
    }

    public void HighlightStep(int step, int counter)
    {
        int highlightLine = -1;
        string explanation = "";

        switch (step)
        {
            case 1:
                highlightLine = 5;
                explanation =
                    "STEP 1 - THREAD A LOCK\n\n" +
                    "Thread A acquires the mutex.\n" +
                    "It can now enter the critical section.";
                break;

            case 2:
                highlightLine = 5;
                explanation =
                    "STEP 2 - THREAD B WAITING\n\n" +
                    "Thread B tries to acquire the mutex, " +
                    "but Thread A owns it.\n" +
                    "Thread B must wait.";
                break;

            case 3:
                highlightLine = 7;
                explanation =
                    "STEP 3 - THREAD A UPDATE\n\n" +
                    "Thread A executes counter++ " +
                    "inside the critical section.\n" +
                    "Shared Counter: " + counter;
                break;

            case 4:
                highlightLine = 8;
                explanation =
                    "STEP 4 - THREAD A UNLOCK\n\n" +
                    "Thread A exits the lock block.\n" +
                    "The mutex is released.";
                break;

            case 5:
                highlightLine = 5;
                explanation =
                    "STEP 5 - THREAD B LOCK\n\n" +
                    "Thread B acquires the available mutex.\n" +
                    "It can now enter the critical section.";
                break;

            case 6:
                highlightLine = 7;
                explanation =
                    "STEP 6 - THREAD B UPDATE\n\n" +
                    "Thread B executes counter++ " +
                    "inside the critical section.\n" +
                    "Shared Counter: " + counter;
                break;

            case 7:
                highlightLine = 8;
                explanation =
                    "STEP 7 - THREAD B UNLOCK\n\n" +
                    "Thread B exits the lock block.\n" +
                    "The mutex is released.\n\n" +
                    "Both increments are saved.\n" +
                    "Final Counter: " + counter;
                break;

            default:
                ShowReady();
                return;
        }

        UpdateCode(highlightLine);

        if (explanationText != null)
            explanationText.text = explanation;
    }

    private void UpdateCode(int highlightedLine)
    {
        if (codeDisplayText == null)
            return;

        System.Text.StringBuilder builder =
            new System.Text.StringBuilder();

        for (int i = 0; i < codeLines.Length; i++)
        {
            string safeLine = codeLines[i]
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");

            if (i == highlightedLine)
            {
                builder.Append("<color=#FFD54F><b>");
                builder.Append(safeLine);
                builder.Append("</b></color>");
            }
            else
            {
                builder.Append(safeLine);
            }

            if (i < codeLines.Length - 1)
                builder.Append("\n");
        }

        codeDisplayText.richText = true;
        codeDisplayText.text = builder.ToString();
    }
}
