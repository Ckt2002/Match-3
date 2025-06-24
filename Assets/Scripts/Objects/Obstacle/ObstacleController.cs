using DG.Tweening;
using System;
using UnityEngine;

public class ObstacleController : MonoBehaviour, IObstacle
{
    [SerializeField] int maxHealth;
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    ObstacleVFX obstacleVFX;
    int currentHealth;
    Vector3 originalScale;
    Action destroyCurrent;

    private void Awake()
    {
        originalScale = transform.localScale;
        obstacleVFX = GetComponentInChildren<ObstacleVFX>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        ResetObstacle();
    }

    public void RegisterAction(Action action)
    {
        this.destroyCurrent += action;
    }

    public void ActiveObstacle()
    {
        gameObject.SetActive(true);
    }

    public void TakeDamage()
    {
        if (obstacleVFX == null)
            obstacleVFX = GetComponentInChildren<ObstacleVFX>();
        obstacleVFX.RunVFX();
        if (currentHealth > 0)
        {
            currentHealth -= 1;
            if (currentHealth - 1 > 0)
                spriteRenderer.sprite = sprites[currentHealth - 1];
        }
        if (currentHealth <= 0)
            Hide();
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, 0.001f).OnComplete(
            () =>
            {
                destroyCurrent.Invoke();
                gameObject.SetActive(false);
            }
            );
    }

    public void ResetObstacle()
    {
        transform.localScale = originalScale;
        spriteRenderer.sprite = sprites[maxHealth - 1];
    }

    public void HideObstacle()
    {
        gameObject.SetActive(false);
    }
}
