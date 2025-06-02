using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int width, height;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    private void Start()
    {
        PoolingController.Instance.CreateGame(width, height);
        ArrangerController.Instance.Arrange(width, height);
    }
}