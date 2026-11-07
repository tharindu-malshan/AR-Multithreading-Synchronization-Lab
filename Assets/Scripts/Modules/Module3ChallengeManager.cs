
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Module3ChallengeManager : MonoBehaviour
{
    [Header("Challenge UI")]
    [SerializeField] private GameObject challengePanel;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text progressText;

    [Header("Answer Buttons")]
    [SerializeField] private Button[] answerButtons;

    [Header("Navigation")]
    [SerializeField] private Button nextButton;

    private int currentQuestion = 0;
    private int score = 0;
    private bool answered = false;
    private bool finished = false;

    private readonly string[] questions =
    {
        "What is a race condition?",

        "Two threads read counter = 0 and both add 1. " +
        "What can the final counter be if both write 1?",

        "Which operation reads the shared counter?",

        "Why is counter++ unsafe when multiple threads " +
        "update the same variable without synchronization?",

        "Which technique can prevent a race condition?"
    };

    private readonly string[,] answers =
    {
        {
            "Threads access shared data with unsafe timing",
            "Only one thread executes",
            "A program has no shared memory",
            "All threads always execute correctly"
        },
        {
            "0",
            "1",
            "2",
            "3"
        },
        {
            "int local = counter;",
            "local = local + 1;",
            "counter = local;",
            "ResetSimulation();"
        },
        {
            "It always runs on the GPU",
            "It is always a single atomic operation",
            "Its read-modify-write steps can overlap",
            "It cannot change a variable"
        },
        {
            "Remove all variables",
            "Use a mutex or lock",
            "Increase the number of threads",
            "Run threads without shared memory protection"
        }
    };

    private readonly int[] correctAnswers =
    {
        0, 1, 0, 2, 1
    };

    private readonly string[] explanations =
    {
        "A race condition occurs when the result depends " +
        "on unsafe timing between concurrent operations.",

        "Both threads read 0 and calculate 1. " +
        "Their writes can leave the final counter at 1. " +
        "This is called a lost update.",

        "int local = counter reads the shared counter " +
        "and stores its value in a local variable.",

        "counter++ involves reading, modifying and writing. " +
        "Without synchronization, operations can overlap.",

        "A mutex or lock allows only one thread at a time " +
        "to update the protected shared data."
    };

    private void Start()
    {
        if (challengePanel != null)
            challengePanel.SetActive(false);

        if (answerButtons != null)
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                int answerIndex = i;

                if (answerButtons[i] != null)
                {
                    answerButtons[i].onClick.AddListener(
                        () => SelectAnswer(answerIndex)
                    );
                }
            }
        }
    }

    public void OpenChallenge()
    {
        if (challengePanel == null)
            return;

        challengePanel.SetActive(true);

        currentQuestion = 0;
        score = 0;
        answered = false;
        finished = false;

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

        questionText.text = questions[currentQuestion];

        scoreText.text = "Score: " + score;

        progressText.text =
            "Question " + (currentQuestion + 1) +
            " / " + questions.Length;

        feedbackText.text = "Select the correct answer.";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i];

            if (button == null)
                continue;

            bool validAnswer = i < 4;

            button.gameObject.SetActive(validAnswer);
            button.interactable = validAnswer;

            if (validAnswer)
            {
                TMP_Text label =
                    button.GetComponentInChildren<TMP_Text>();

                if (label != null)
                    label.text = answers[currentQuestion, i];
            }
        }

        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(true);
            nextButton.interactable = false;

            TMP_Text nextLabel =
                nextButton.GetComponentInChildren<TMP_Text>();

            if (nextLabel != null)
                nextLabel.text = "NEXT";
        }
    }

    public void SelectAnswer(int selectedIndex)
    {
        if (answered || finished)
            return;

        if (selectedIndex < 0 || selectedIndex >= 4)
            return;

        answered = true;

        bool correct =
            selectedIndex == correctAnswers[currentQuestion];

        if (correct)
        {
            score++;

            feedbackText.text =
                "CORRECT!\n" +
                explanations[currentQuestion];
        }
        else
        {
            feedbackText.text =
                "INCORRECT!\n" +
                "Correct answer: " +
                answers[
                    currentQuestion,
                    correctAnswers[currentQuestion]
                ] +
                "\n" +
                explanations[currentQuestion];
        }

        scoreText.text = "Score: " + score;

        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.interactable = false;
        }

        if (nextButton != null)
        {
            nextButton.interactable = true;

            TMP_Text label =
                nextButton.GetComponentInChildren<TMP_Text>();

            if (label != null)
            {
                label.text =
                    currentQuestion == questions.Length - 1
                    ? "FINISH"
                    : "NEXT";
            }
        }
    }

    public void NextQuestion()
    {
        if (finished)
        {
            OpenChallenge();
            return;
        }

        if (!answered)
            return;

        currentQuestion++;

        if (currentQuestion >= questions.Length)
        {
            FinishChallenge();
        }
        else
        {
            ShowQuestion();
        }
    }

    private void FinishChallenge()
    {
        finished = true;

        int previousBest =
            PlayerPrefs.GetInt("Module3ChallengeBestScore", 0);

        if (score > previousBest)
        {
            PlayerPrefs.SetInt(
                "Module3ChallengeBestScore",
                score
            );

            PlayerPrefs.Save();
        }

        int bestScore =
            PlayerPrefs.GetInt("Module3ChallengeBestScore", 0);

        questionText.text = "CHALLENGE COMPLETED!";

        progressText.text = "5 / 5 Completed";

        scoreText.text =
            "Final Score: " + score + " / 5";

        feedbackText.text =
            "Your Score: " + score + " / 5\n" +
            "Best Score: " + bestScore + " / 5\n\n" +
            GetFinalFeedback();

        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.gameObject.SetActive(false);
        }

        if (nextButton != null)
        {
            nextButton.interactable = true;

            TMP_Text label =
                nextButton.GetComponentInChildren<TMP_Text>();

            if (label != null)
                label.text = "TRY AGAIN";
        }
    }

    private string GetFinalFeedback()
    {
        if (score == 5)
            return "Excellent! You understand race conditions.";

        if (score >= 3)
            return "Good work! Review the incorrect answers.";

        return "Keep practicing! Try the simulation again.";
    }
}
