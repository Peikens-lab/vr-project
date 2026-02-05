using UnityEngine;
using TMPro;

public class AnswerObject : MonoBehaviour
{
    public TextMeshPro numberText;
    private bool isCorrect;

    private MathQuestionManager questionManager;
    private GameOverManager gameOverManager;

    void Start()
    {
        questionManager = FindObjectOfType<MathQuestionManager>();
        gameOverManager = FindObjectOfType<GameOverManager>();
    }

    public void SetAnswer(int value, bool correct)
    {
        numberText.text = value.ToString();
        isCorrect = correct;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Sword")) return;
        if (!questionManager.CanAnswer()) return;

        if (isCorrect)
            questionManager.AddScore();
        else
            gameOverManager.ShowGameOver();
    }
}
