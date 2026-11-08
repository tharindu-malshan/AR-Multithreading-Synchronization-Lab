
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Module5ChallengeManager : MonoBehaviour
{
    [Header("Challenge Panels")]
    [SerializeField] private GameObject challengePanel;
    [SerializeField] private GameObject openChallengeButton;

    [Header("Challenge Text")]
    [SerializeField] private TMP_Text challengeTitle;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text progressText;

    [Header("Answer Buttons")]
    [SerializeField] private Button[] answerButtons;

    [Header("Navigation")]
    [SerializeField] private Button nextQuestionButton;

    private int currentQuestion = 0;
    private int score = 0;
    private bool answered = false;

    private readonly string[] questions =
    {
        "What is the main purpose of a semaphore?",
        "A semaphore has 2 permits. How many threads can acquire permits at the same time?",
        "What happens when Thread C requests a permit while both permits are in use?",
        "Which method releases a permit in C# SemaphoreSlim?",
        "Why should semaphore.Release() be placed inside a finally block?"
    };

    private readonly string[,] answers =
    {
        {
            "Control access to shared resources",
            "Increase CPU speed",
            "Create a new database",
            "Stop all threads"
        },
        {
            "Only 1 thread",
            "2 threads",
            "3 threads",
            "Unlimited threads"
        },
        {
            "Thread C gets a permit immediately",
            "Thread C waits",
            "The semaphore is deleted",
            "All threads stop permanently"
        },
        {
            "WaitAsync()",
            "Start()",
            "Release()",
            "Reset()"
        },
        {
            "To release the permit even if an exception occurs",
            "To increase the number of threads",
            "To disable the semaphore",
            "To make the program run faster"
        }
    };

    private readonly int[] correctAnswers = { 0, 1, 1, 2, 0 };

    private void Start()
    {
        if (challengePanel != null)
            challengePanel.SetActive(false);
    }

    public void OpenChallenge()
    {
        currentQuestion = 0;
        score = 0;
        answered = false;

        if (challengePanel != null)
            challengePanel.SetActive(true);

        ShowQuestion();
    }

    public void CloseChallenge()
    {
        if (challengePanel != null)
            challengePanel.SetActive(false);
    }

    private void ShowQuestion()
    {
        answered = false;

        if (challengeTitle != null)
            challengeTitle.text = "SEMAPHORE CHALLENGE";

        if (questionText != null)
            questionText.text = questions[currentQuestion];

        if (scoreText != null)
            scoreText.text = "SCORE: " + score + " / " + questions.Length;

        if (progressText != null)
            progressText.text = "QUESTION " +
                (currentQuestion + 1) + " / " + questions.Length;

        if (feedbackText != null)
            feedbackText.text = "Select the correct answer.";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] == null)
                continue;

            answerButtons[i].interactable = true;

            TMP_Text buttonText =
                answerButtons[i].GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
                buttonText.text = answers[currentQuestion, i];
        }

        if (nextQuestionButton != null)
        {
            nextQuestionButton.gameObject.SetActive(true);
            nextQuestionButton.interactable = false;

            TMP_Text nextText =
                nextQuestionButton.GetComponentInChildren<TMP_Text>();

            if (nextText != null)
            {
                nextText.text =
                    currentQuestion == questions.Length - 1
                    ? "FINISH"
                    : "NEXT QUESTION";
            }
        }
    }

    public void SelectAnswer(int selectedIndex)
    {
        if (answered)
            return;

        if (selectedIndex < 0 || selectedIndex >= 4)
            return;

        answered = true;

        bool isCorrect =
            selectedIndex == correctAnswers[currentQuestion];

        if (isCorrect)
        {
            score++;

            if (feedbackText != null)
                feedbackText.text = "CORRECT ANSWER!";
        }
        else
        {
            if (feedbackText != null)
            {
                feedbackText.text =
                    "WRONG ANSWER!\nCorrect answer: " +
                    answers[currentQuestion,
                    correctAnswers[currentQuestion]];
            }
        }

        if (scoreText != null)
            scoreText.text = "SCORE: " + score + " / " + questions.Length;

        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.interactable = false;
        }

        if (nextQuestionButton != null)
            nextQuestionButton.interactable = true;
    }

    public void NextQuestion()
    {
        if (!answered)
            return;

        currentQuestion++;

        if (currentQuestion >= questions.Length)
        {
            ShowFinalResult();
        }
        else
        {
            ShowQuestion();
        }
    }

    private void ShowFinalResult()
    {
        if (challengeTitle != null)
            challengeTitle.text = "CHALLENGE COMPLETED!";

        if (questionText != null)
            questionText.text = "You completed all 5 questions.";

        if (scoreText != null)
            scoreText.text = "FINAL SCORE: " + score + " / " + questions.Length;

        if (progressText != null)
            progressText.text = "COMPLETED";

        if (feedbackText != null)
        {
            if (score == 5)
                feedbackText.text = "EXCELLENT! Perfect score!";
            else if (score >= 3)
                feedbackText.text = "GOOD JOB! Keep learning!";
            else
                feedbackText.text = "KEEP PRACTICING!";
        }

        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.gameObject.SetActive(false);
        }

        if (nextQuestionButton != null)
            nextQuestionButton.gameObject.SetActive(false);
    }

    public void RestartChallenge()
    {
        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.gameObject.SetActive(true);
        }

        OpenChallenge();
    }
}
