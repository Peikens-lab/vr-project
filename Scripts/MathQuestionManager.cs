using UnityEngine;
using TMPro;
using System.Collections;

public class MathQuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public AnswerObject[] answerObjects;

    private int score = 0;
    private int highScore = 0;
    private bool canAnswer = true;

    enum MathType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreText();
        UpdateHighScoreText();
        GenerateQuestion();
    }

    public void AddScore()
    {
        if (!canAnswer) return;

        canAnswer = false;

        score += 1;
        UpdateScoreText();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }

        GenerateQuestion();
        StartCoroutine(AnswerCooldown());
    }

    private IEnumerator AnswerCooldown()
    {
        yield return new WaitForSeconds(2f);
        canAnswer = true;
    }

    public bool CanAnswer()
    {
        return canAnswer;
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore;
    }

    public void GenerateQuestion()
    {
        MathType mathType = (MathType)Random.Range(0, 4);

        int a = 0;
        int b = 0;
        int correctAnswer = 0;
        string symbol = "";

        switch (mathType)
        {
            case MathType.Add:
                a = Random.Range(1, 10);
                b = Random.Range(1, 10);
                correctAnswer = a + b;
                symbol = "+";
                break;

            case MathType.Subtract:
                a = Random.Range(1, 10);
                b = Random.Range(1, a + 1);
                correctAnswer = a - b;
                symbol = "−";
                break;

            case MathType.Multiply:
                a = Random.Range(1, 10);
                b = Random.Range(1, 10);
                correctAnswer = a * b;
                symbol = "×";
                break;

            case MathType.Divide:
                b = Random.Range(1, 10);
                correctAnswer = Random.Range(1, 10);
                a = b * correctAnswer;
                symbol = "÷";
                break;
        }

        questionText.text = $"{a} {symbol} {b} = ?";

        int correctIndex = Random.Range(0, answerObjects.Length);

        for (int i = 0; i < answerObjects.Length; i++)
        {
            if (i == correctIndex)
            {
                answerObjects[i].SetAnswer(correctAnswer, true);
            }
            else
            {
                int wrong;
                do
                {
                    wrong = Random.Range(1, 20);
                } while (wrong == correctAnswer);

                answerObjects[i].SetAnswer(wrong, false);
            }
        }
    }
}
