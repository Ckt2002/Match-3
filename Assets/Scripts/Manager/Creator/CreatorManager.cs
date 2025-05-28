using UnityEngine;

public class CreatorManager : MonoBehaviour
{
    [SerializeField] private int heightSize;
    [SerializeField] private int widthSize;
    [SerializeField] private float spacing = 1;
    [SerializeField] private Vector3 spawnPosOffset = Vector3.zero;

    private ICreator[] creators;

    private void Start()
    {
        creators = GetComponents<ICreator>();
        Create();
    }

    private void Create()
    {
        BoardController boardController = BoardController.Instance;
        foreach (ICreator creator in creators)
            creator.Create(widthSize, heightSize, spacing, spawnPosOffset, boardController);
        boardController.boardGrid.InitGridSize(widthSize, heightSize);
    }
}
