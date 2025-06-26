using DG.Tweening;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    [SerializeField] Vector3 animationScale;
    [SerializeField] float duration;
    public int requireScore;
    ParticleSystem particle;
    Vector3 originalScale;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        originalScale = transform.localScale;
    }

    public void RunAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 0, -360), duration, RotateMode.FastBeyond360));
        sequence.Join(transform.DOScale(animationScale, duration)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale, duration / 10);
                particle?.Play();
            }));
    }

    public void EndAnimation() => DOTween.Kill(transform);
}
