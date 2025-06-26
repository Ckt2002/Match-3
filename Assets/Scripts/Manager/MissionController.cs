using System;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public static MissionController Instance;
    public Action<Vector3, EPotion?, EObstacle?> OnUpdateScoreItems;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateMission(Vector3 position, EPotion? potionType = null, EObstacle? obstacleType = null)
    {
        OnUpdateScoreItems.Invoke(position, potionType, obstacleType);
    }
}