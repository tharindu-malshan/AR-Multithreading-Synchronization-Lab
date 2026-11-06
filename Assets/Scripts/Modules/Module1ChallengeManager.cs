
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Module1ChallengeManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject challengePanel;

    [Header("Text")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Answer Buttons")]
    [SerializeField] private Button[] answerButtons;

    [Header("Next Button")]
    [SerializeField] private Button nextQuestionButton;

    private int currentQuestion;
    private int score;
    private bool answered;

    private readonly string[] questions =
    {
        "Which state comes immediately after NEW?",
        "Which state means a thread is executing?",
        "What happens after a thread completes its task?"
    };

    private readonly string[,] answers =
    {
        { "RUNNING", "READY", "WAITING", "TERMINATED" },
        { "NEW", "READY", "RUNNING", "WAITING" },
        { "READY", "RUNNING", "NEW", "TERMINATED" }
    };

    private readonly int[] correctAnswers = { 1, 2, 3 };

    public void OpenChallenge()
    {
        currentQuestion = 0;
        score = 0;

        challengePanel.SetActive(true);
        ShowQuestion();
    }

    public void CloseChallenge()
    {
        challengePanel.SetActive(false);
    }

    private void ShowQuestion()
    {
        answered = false;

        questionText.text = questions[currentQuestion];
        feedbackText.text = "Choose the correct answer.";
        scoreText.text = "SCORE: " + score + " / " + questions.Length;

        nextQuestionButton.gameObject.SetActive(false);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = true;

            TMP_Text label =
                answerButtons[i].GetComponentInChildren<TMP_Text>();

            if (label != null)
                label.text = answers[currentQuestion, i];
        }
    }

    public void SelectAnswer(int answerIndex)
    {
        if (answered)
            return;

        answered = true;

        bool correct =
            answerIndex == correctAnswers[currentQuestion];

        if (correct)
        {
            score++;
            feedbackText.text = "Correct! Well done.";
        }
        else
        {
            feedbackText.text =
                "Incorrect. The correct answer is: " +
                answers[currentQuestion, correctAnswers[currentQuestion]];
        }

        scoreText.text = "SCORE: " + score + " / " + questions.Length;

        foreach (Button button in answerButtons)
            button.interactable = false;

        nextQuestionButton.gameObject.SetActive(true);

        if (currentQuestion == questions.Length - 1)
        {
            nextQuestionButton.GetComponentInChildren<TMP_Text>().text =
                "FINISH";
        }
        else
        {
            nextQuestionButton.GetComponentInChildren<TMP_Text>().text =
                "NEXT QUESTION";
        }
    }

    public void NextQuestion()
    {
        if (!answered)
            return;

        currentQuestion++;

        if (currentQuestion >= questions.Length)
        {
            feedbackText.text =
                "Challenge completed! Final score: " +
                score + " / " + questions.Length;

            nextQuestionButton.gameObject.SetActive(false);

            PlayerPrefs.SetInt("Module1ChallengeScore", score);
            PlayerPrefs.Save();

            return;
        }

        ShowQuestion();
    }
}
