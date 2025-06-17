#if UNITY_EDITOR
using UnityEngine;

public class TileTool : MonoBehaviour
{
    public ObstacleType obstacleType;
    public GameObject[] potionsPrefab;
    public EPotion potionType;
    public Transform potionParent;

    [ContextMenu("Assign obstacle")]
    public void AssignObstacle()
    {
        GetComponent<TileController>().obstacles = GetComponentsInChildren<IObstacle>(true);
        GetComponent<TileController>().AssignObstacleTool((int)obstacleType);
    }

    [ContextMenu("Clear obstacle")]
    public void ClearObstacle()
    {
        GetComponent<TileController>().ClearObstacleTool();
    }

    [ContextMenu("Clear tile")]
    public void ClearTile()
    {
        GetComponent<TileController>().ClearTileTool();
    }

    [ContextMenu("Assign potion")]
    public void AssignPotion()
    {
        potionParent = GameObject.Find("Potions").transform;
        GameObject go = Instantiate(potionsPrefab[(int)potionType], potionParent);
        go.transform.position = transform.position;
        GetComponent<TileController>().AssignObstacleTool((int)obstacleType);
    }
}
#endif