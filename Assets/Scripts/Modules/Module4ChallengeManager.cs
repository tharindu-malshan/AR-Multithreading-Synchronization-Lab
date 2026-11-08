
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Module4ChallengeManager : MonoBehaviour
{
    [Header("Quiz UI")]
    [SerializeField] private GameObject challengePanel;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text progressText;

    [Header("Answer Buttons")]
    [SerializeField] private Button[] answerButtons;

    [Header("Navigation")]
    [SerializeField] private Button nextButton;

    private string[] questions =
    {
        "What is the main purpose of a mutex?",
        "What happens when Thread B tries to acquire a mutex already owned by Thread A?",
        "What is a critical section?",
        "What happens when a C# lock block finishes?",
        "Two threads safely increment a counter from 0 using the same mutex. What is the final value?"
    };

    private string[,] answers =
    {
        {
            "Increase thread speed",
            "Protect shared resources",
            "Delete shared variables",
            "Create new threads"
        },
        {
            "Thread B enters immediately",
            "Both threads enter together",
            "Thread B waits",
            "The counter resets"
        },
        {
            "A section of code accessing shared resources",
            "A method that creates threads",
            "A type of variable",
            "A Unity GameObject"
        },
        {
            "The mutex is released",
            "The mutex stays locked forever",
            "A new thread is created",
            "The counter becomes zero"
        },
        {
            "0",
            "1",
            "2",
            "4"
        }
    };

    private int[] correctAnswers = { 1, 2, 0, 0, 2 };

    private int currentQuestion = 0;
    private int score = 0;
    private bool answered = false;
    private bool quizFinished = false;

    private const string BestScoreKey = "Module4ChallengeBestScore";

    private void Start()
    {
        if (challengePanel != null)
            challengePanel.SetActive(false);
    }

    public void OpenChallenge()
    {
        if (challengePanel == null)
            return;

        challengePanel.SetActive(true);
        ResetQuiz();
    }

    public void CloseChallenge()
    {
        if (challengePanel != null)
            challengePanel.SetActive(false);
    }

    public void ResetQuiz()
    {
        currentQuestion = 0;
        score = 0;
        answered = false;
        quizFinished = false;

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        answered = false;

        if (questionText != null)
            questionText.text = questions[currentQuestion];

        if (progressText != null)
            progressText.text =
                "QUESTION " + (currentQuestion + 1) +
                " / " + questions.Length;

        if (feedbackText != null)
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
                TMP_Text buttonText =
                    button.GetComponentInChildren<TMP_Text>();

                if (buttonText != null)
                    buttonText.text = answers[currentQuestion, i];
            }
        }

        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(true);
            nextButton.interactable = false;

            TMP_Text nextText =
                nextButton.GetComponentInChildren<TMP_Text>();

            if (nextText != null)
                nextText.text = "NEXT QUESTION";
        }

        UpdateScore();
    }

    public void SelectAnswer(int answerIndex)
    {
        if (answered || quizFinished)
            return;

        if (answerIndex < 0 || answerIndex >= 4)
            return;

        answered = true;

        bool isCorrect =
            answerIndex == correctAnswers[currentQuestion];

        if (isCorrect)
        {
            score++;

            if (feedbackText != null)
                feedbackText.text = "Correct! Well done.";
        }
        else
        {
            if (feedbackText != null)
            {
                feedbackText.text =
                    "Incorrect. Correct answer: " +
                    answers[currentQuestion,
                            correctAnswers[currentQuestion]];
            }
        }

        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.interactable = false;
        }

        if (nextButton != null)
            nextButton.interactable = true;

        UpdateScore();
    }

    public void NextQuestion()
    {
        if (quizFinished)
        {
            ResetQuiz();
            return;
        }

        if (!answered)
            return;

        currentQuestion++;

        if (currentQuestion >= questions.Length)
        {
            FinishQuiz();
        }
        else
        {
            ShowQuestion();
        }
    }

    private void FinishQuiz()
    {
        quizFinished = true;

        int bestScore =
            PlayerPrefs.GetInt(BestScoreKey, 0);

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.Save();
        }

        if (questionText != null)
        {
            questionText.text =
                "CHALLENGE COMPLETED!\n\n" +
                "Your Score: " + score + " / 5\n" +
                "Best Score: " + bestScore + " / 5";
        }

        if (progressText != null)
            progressText.text = "QUIZ FINISHED";

        if (feedbackText != null)
        {
            feedbackText.text = score >= 4
                ? "Excellent! You understand mutex synchronization."
                : "Good effort! Review the simulation and try again.";
        }

        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.gameObject.SetActive(false);
        }

        if (nextButton != null)
        {
            nextButton.interactable = true;

            TMP_Text nextText =
                nextButton.GetComponentInChildren<TMP_Text>();

            if (nextText != null)
                nextText.text = "TRY AGAIN";
        }

        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score + " / 5";
    }
}
