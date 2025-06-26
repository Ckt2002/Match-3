using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] StarUI[] stars;
    [SerializeField] Slider progressBar;
    [SerializeField] float duration;
    int currentStarIndex = 0;

    private void Start()
    {
        ScoreUI.Instance.OnUpdateScore += OnUpdateScore;
    }

    // Setup star require score, reset progressbar
    public void SetUpProgressBar()
    {
        progressBar.value = 0;
    }

    public void OnUpdateScore(int currentScore, int maxScore)
    {
        Debug.Log((float)currentScore / maxScore);
        progressBar.DOValue((float)currentScore / maxScore, duration)
            .OnComplete(() =>
            {
                if (currentStarIndex <= stars.Length - 1)
                {
                    StarUI star = stars[currentStarIndex];
                    if (currentScore >= stars[currentStarIndex].requireScore)
                        stars[currentStarIndex++].RunAnimation();
                }
            });
    }
}
