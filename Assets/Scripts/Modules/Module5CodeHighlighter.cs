
using UnityEngine;
using TMPro;
using System.Text;

public class Module5CodeHighlighter : MonoBehaviour
{
    [Header("Code Panel References")]
    [SerializeField] private TMP_Text codeDisplayText;
    [SerializeField] private TMP_Text explanationText;

    private readonly string[] codeLines =
    {
        "using System.Threading;",
        "",
        "SemaphoreSlim semaphore = new SemaphoreSlim(2, 2);",
        "",
        "async Task UseResourceAsync()",
        "{",
        "    await semaphore.WaitAsync();",
        "    try",
        "    {",
        "        // Access a shared resource",
        "        UseResource();",
        "    }",
        "    finally",
        "    {",
        "        semaphore.Release();",
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
                "SEMAPHORE & RESOURCE MANAGEMENT\n\n" +
                "Two permits are available.\n" +
                "Thread A and Thread B can use resources.\n" +
                "Thread C must wait when both permits are used.\n\n" +
                "Press START to begin.";
        }
    }

    public void HighlightStep(int step, int availablePermits)
    {
        int highlightLine = -1;
        string explanation = "";

        switch (step)
        {
            case 1:
                highlightLine = 6;
                explanation =
                    "STEP 1 - THREAD A\n\n" +
                    "Thread A acquires a semaphore permit.\n" +
                    "Resource 1 is assigned to Thread A.";
                break;

            case 2:
                highlightLine = 6;
                explanation =
                    "STEP 2 - THREAD B\n\n" +
                    "Thread B acquires the second permit.\n" +
                    "Resource 2 is assigned to Thread B.";
                break;

            case 3:
                highlightLine = 6;
                explanation =
                    "STEP 3 - THREAD C WAITING\n\n" +
                    "Thread C calls WaitAsync().\n" +
                    "No permits are available, so it waits.";
                break;

            case 4:
                highlightLine = 14;
                explanation =
                    "STEP 4 - THREAD A RELEASE\n\n" +
                    "Thread A finishes using Resource 1.\n" +
                    "It releases its semaphore permit.";
                break;

            case 5:
                highlightLine = 6;
                explanation =
                    "STEP 5 - THREAD C\n\n" +
                    "A permit is now available.\n" +
                    "Thread C acquires it and uses Resource 1.";
                break;

            case 6:
                highlightLine = 14;
                explanation =
                    "STEP 6 - THREAD B RELEASE\n\n" +
                    "Thread B releases Resource 2.\n" +
                    "One permit becomes available.";
                break;

            case 7:
                highlightLine = 14;
                explanation =
                    "STEP 7 - THREAD C RELEASE\n\n" +
                    "Thread C releases Resource 1.\n" +
                    "All threads are completed.";
                break;

            default:
                ShowReady();
                return;
        }

        UpdateCode(highlightLine);

        if (explanationText != null)
        {
            explanationText.text =
                explanation +
                "\n\nAVAILABLE PERMITS: " +
                availablePermits + " / 2";
        }
    }

    private void UpdateCode(int highlightedLine)
    {
        if (codeDisplayText == null)
            return;

        StringBuilder builder = new StringBuilder();

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
