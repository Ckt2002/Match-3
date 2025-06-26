using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text text;
    [SerializeField] Sprite[] potionSprites;
    [SerializeField] Sprite[] obstacleSprites;
    [SerializeField] GameObject trailPrefab;

    RectTransform trailContainer;
    List<TrailAnimation> trails;
    EPotion? potionType = null;
    EObstacle? obstacleType = null;

    private int currentNumber;

    private void Awake()
    {
        trails = new();
    }

    public void SetupMissionItem(int number, EPotion? potionType = null, EObstacle? obstacleType = null)
    {
        currentNumber = number;
        text.text = $"X{number}";
        if (potionType != null)
        {
            image.sprite = potionSprites[(int)potionType];
            this.potionType = potionType;
        }
        else
        {
            image.sprite = obstacleSprites[(int)obstacleType];
            this.obstacleType = obstacleType;
        }
        trailContainer = GameObject.Find("TrailContainer").GetComponent<RectTransform>();
        MissionController.Instance.OnUpdateScoreItems += CreateTrails;
    }

    private void CreateTrails(Vector3 spawnPos, EPotion? potionType = null, EObstacle? obstacleType = null)
    {
        bool potionValid = potionType != null && potionType.Value == this.potionType.Value;
        bool obstacleValid = obstacleType != null && obstacleType.Value == this.obstacleType.Value;
        if (currentNumber > 0 && (potionValid || obstacleValid))
        {
            GameObject go = null;
            foreach (var item in trails)
                if (!item.gameObject.activeInHierarchy)
                {
                    go = item.gameObject;
                    break;
                }

            if (go == null)
                go = Instantiate(trailPrefab, trailContainer);

            go.GetComponent<RectTransform>().anchoredPosition = spawnPos;
            var trail = go.GetComponent<TrailAnimation>();
            trails.Add(trail);
            go.SetActive(true);
            Vector2 localPos = trailContainer.InverseTransformPoint(transform.position);
            trail.SetupTargetPos(localPos);
            trail.OnMoveComplete += UpdateUI;
        }
    }

    public void UpdateUI()
    {
        this.currentNumber = Mathf.Max(this.currentNumber - 1, 0);
        text.text = $"X{currentNumber}";
    }
}
