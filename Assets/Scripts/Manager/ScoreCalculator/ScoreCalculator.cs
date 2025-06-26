using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public static ScoreCalculator Instance { get; private set; }
    public int maxScore = 10;
    int currentScore = 0;
    IScoreUI scoreUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        scoreUI = ScoreUI.Instance;
    }

    public void UpdateScore(int score)
    {
        if (currentScore < maxScore)
            currentScore += score;

        scoreUI.ScoreChange(currentScore, maxScore);
    }
}
