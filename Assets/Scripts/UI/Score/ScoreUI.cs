using System;
using UnityEngine;

public class ScoreUI : MonoBehaviour, IScoreUI
{
    public static ScoreUI Instance;
    public Action<int, int> OnUpdateScore;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ScoreChange(int currentScore, int maxScore)
    {
        OnUpdateScore.Invoke(currentScore, maxScore);
    }
}
