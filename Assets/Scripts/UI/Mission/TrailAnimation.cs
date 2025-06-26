using System;
using System.Collections;
using UnityEngine;

public class TrailAnimation : MonoBehaviour
{
    [SerializeField] float speed;

    public Action OnMoveComplete;

    private RectTransform rectTransform;
    private Vector3 targetPos;

    public void SetupTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(RunAnimation());
    }

    private IEnumerator RunAnimation()
    {
        while (rectTransform.anchoredPosition != (Vector2)targetPos)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                targetPos,
                speed * Time.deltaTime
            );
            yield return null;
        }

        OnMoveComplete.Invoke();
        gameObject.SetActive(false);
    }
}