
using UnityEngine;
using TMPro;

public class Module2CodeHighlighter : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text codeDisplayText;
    public TMP_Text explanationText;

    private const string NormalColor = "#D1D5DB";
    private const string HighlightColor = "#4ADE80";

    private void OnEnable()
    {
        ShowReady();
    }

    public void ShowReady()
    {
        DisplayCode(-1);

        if (explanationText != null)
        {
            explanationText.text =
                "Both threads are ready. Select a thread " +
                "to update the shared counter.";
        }
    }

    public void HighlightThreadA(int counter)
    {
        DisplayCode(4);

        if (explanationText != null)
        {
            explanationText.text =
                "Thread A accessed shared memory. " +
                "Counter increased to " + counter + ".";
        }
    }

    public void HighlightThreadB(int counter)
    {
        DisplayCode(9);

        if (explanationText != null)
        {
            explanationText.text =
                "Thread B accessed shared memory. " +
                "Counter increased to " + counter + ".";
        }
    }

    private void DisplayCode(int highlightedLine)
    {
        string[] lines =
        {
            "int counter = 0;",
            "",
            "void ThreadA()",
            "{",
            "    counter++;",
            "}",
            "",
            "void ThreadB()",
            "{",
            "    counter++;",
            "}"
        };

        string result = "";

        for (int i = 0; i < lines.Length; i++)
        {
            string color =
                i == highlightedLine
                ? HighlightColor
                : NormalColor;

            string safeLine = lines[i]
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");

            result +=
                "<color=" + color + ">" +
                safeLine +
                "</color>\n";
        }

        if (codeDisplayText != null)
            codeDisplayText.text = result;
    }
}
