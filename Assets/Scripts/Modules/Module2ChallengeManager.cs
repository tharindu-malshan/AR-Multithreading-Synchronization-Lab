
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Module2ChallengeManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject challengePanel;

    [Header("Question UI")]
    public TMP_Text questionText;
    public TMP_Text scoreText;
    public TMP_Text feedbackText;
    public TMP_Text progressText;

    [Header("Answer Buttons")]
    public Button[] answerButtons;

    [Header("Navigation")]
    public Button nextButton;

    private int currentQuestion = 0;
    private int score = 0;
    private bool answered = false;

    private readonly string[] questions =
    {
        "What is concurrent execution?",
        "What is shared memory in multithreading?",
        "The counter starts at 0. Thread A increments twice and Thread B once. What is the final value in our Module 2 simulation?",
        "What happens when Thread A and Thread B execute in a different order in our Module 2 simulation?",
        "Why is synchronization important when multiple threads update shared memory?"
    };

    private readonly string[,] options =
    {
        {
            "Only one thread exists",
            "Multiple threads make progress during overlapping time periods",
            "All threads must finish simultaneously",
            "Threads cannot share memory"
        },
        {
            "Memory accessible by multiple threads",
            "Memory used by only one thread",
            "Memory that cannot be modified",
            "Memory stored only on a hard disk"
        },
        {
            "1",
            "2",
            "3",
            "4"
        },
        {
            "The counter always resets",
            "The final counter stays the same for the same number of increments",
            "Thread B stops working",
            "The shared memory is deleted"
        },
        {
            "To change thread colors",
            "To prevent incorrect results caused by conflicting access",
            "To remove all threads",
            "To stop the application"
        }
    };

    private readonly int[] correctAnswers =
    {
        1, 0, 2, 1, 1
    };

    private readonly string[] explanations =
    {
        "Concurrent execution allows threads to make progress during overlapping time periods.",
        "Shared memory allows multiple threads to access the same data.",
        "Three completed increments increase the counter from 0 to 3.",
        "In Module 2, increments are completed one at a time, so the order does not change the final result.",
        "Synchronization coordinates shared memory access and helps prevent race conditions."
    };

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

        if (questionText != null)
            questionText.text = questions[currentQuestion];

        if (scoreText != null)
            scoreText.text = "SCORE: " + score;

        if (progressText != null)
            progressText.text =
                "QUESTION " + (currentQuestion + 1) +
                " / " + questions.Length;

        if (feedbackText != null)
            feedbackText.text = "Select the correct answer.";

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        for (int i = 0; i < answerButtons.Length && i < 4; i++)
        {
            int answerIndex = i;

            Button button = answerButtons[i];

            if (button == null)
                continue;

            button.gameObject.SetActive(true);
            button.interactable = true;

            TMP_Text buttonText =
                button.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
                buttonText.text = options[currentQuestion, i];

            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(
                () => SelectAnswer(answerIndex)
            );
        }
    }

    public void SelectAnswer(int selectedAnswer)
    {
        if (answered)
            return;

        answered = true;

        bool correct =
            selectedAnswer == correctAnswers[currentQuestion];

        if (correct)
            score++;

        if (feedbackText != null)
        {
            feedbackText.text =
                (correct ? "CORRECT! " : "INCORRECT! ") +
                explanations[currentQuestion];
        }

        if (scoreText != null)
            scoreText.text = "SCORE: " + score;

        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.interactable = false;
        }

        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(true);

            TMP_Text nextText =
                nextButton.GetComponentInChildren<TMP_Text>();

            if (nextText != null)
            {
                nextText.text =
                    currentQuestion == questions.Length - 1
                    ? "FINISH"
                    : "NEXT QUESTION";
            }
        }
    }

    public void NextQuestion()
    {
        if (!answered)
            return;

        currentQuestion++;

        if (currentQuestion >= questions.Length)
        {
            FinishChallenge();
            return;
        }

        ShowQuestion();
    }

    private void FinishChallenge()
    {
        PlayerPrefs.SetInt("Module2ChallengeScore", score);
        PlayerPrefs.Save();

        if (questionText != null)
            questionText.text = "CHALLENGE COMPLETED!";

        if (progressText != null)
            progressText.text = "5 / 5 QUESTIONS";

        if (scoreText != null)
            scoreText.text = "FINAL SCORE: " + score + " / 5";

        if (feedbackText != null)
        {
            feedbackText.text =
                score == 5
                ? "Excellent! You understand concurrent execution."
                : score >= 3
                ? "Good work! Review the concepts and try again."
                : "Keep practicing shared memory concepts.";
        }

        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.gameObject.SetActive(false);
        }

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        Debug.Log(
            "Module 2 Challenge Score: " + score + "/5"
        );
    }
}
