using UnityEngine;
using UnityEngine.UI;

public class ScoreTextUI : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        ScoreUI.Instance.OnUpdateScore += OnUpdateScore;
        Reset();
    }

    public void Reset()
    {
        text.text = 0.ToString();
    }

    private void OnUpdateScore(int currentScore, int maxScore)
    {
        text.text = currentScore.ToString();
    }
}
