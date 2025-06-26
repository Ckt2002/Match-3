using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public static MissionController Instance { get; private set; }

    MissionItem[] missionItems;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        missionItems = GetComponentsInChildren<MissionItem>();
    }

    public void SetupMission(List<PotionRequire> potionRequires, List<ObstacleRequire> obstacleRequires)
    {
        int currentIndex = 0;
        foreach (var item in potionRequires)
        {
            missionItems[currentIndex].SetupMissionItem(item.number, item.potionType);
            currentIndex++;
        }

        foreach (var item in obstacleRequires)
        {
            missionItems[currentIndex].SetupMissionItem(item.number, null, item.obstacleType);
            currentIndex++;
        }

        for (int i = currentIndex; i < missionItems.Length; i++)
        {
            missionItems[i].gameObject.SetActive(false);
        }
    }
}
