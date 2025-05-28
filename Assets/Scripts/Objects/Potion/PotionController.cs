using UnityEngine;

public class PotionController : MonoBehaviour
{
    public EPotionType potionType;
    public Vector3 originalMousePos { get; set; } = Vector3.zero;
    public Vector2Int potionIndex { get; private set; } = Vector2Int.zero;
    public BoardController boardController { get; private set; }

    [SerializeField] private Vector3 originalScale = Vector3.one;
    [SerializeField] private Vector3 mouseEnterScale = Vector3.one;
    [SerializeField] private float scaleDuration = 0.3f;

    private bool isSelected, isDragging = false;
    private PotionStateMachine stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<PotionStateMachine>();
        boardController = BoardController.Instance;
    }

    private void OnMouseEnter()
    {
        stateMachine.ChangeState(new PotionMouseEnterState(this, scaleDuration, mouseEnterScale));
    }

    private void OnMouseExit()
    {
        stateMachine.ChangeState(new PotionMouseExitState(this, scaleDuration, originalScale));
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            isSelected = true;
            originalMousePos = Input.mousePosition;
            boardController.SetSelectedPotion(this);
        }
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
        {
            stateMachine.ChangeState(new PotionDragState(this));
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        stateMachine.ChangeState(new PotionReleaseState(this));
        isSelected = false;
        isDragging = false;
    }

    public void Move(Vector3 targetPos)
    {
        stateMachine.ChangeState(new PotionMoveState(this, targetPos));
    }

    public void SetPotionIndex(Vector2Int newIndex)
    {
        potionIndex = newIndex;
    }
}
