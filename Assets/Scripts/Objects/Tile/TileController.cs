using UnityEngine;

public class TileController : MonoBehaviour
{
    public PotionController potion { get; private set; }
    public Vector2Int tileIndex { get; private set; }
    public Vector3 originalMousePos { get; set; } = Vector3.zero;
    public BoardController boardController { get; private set; }
    public Vector3 potionMouseEnterScale = Vector3.one;
    public Vector3 potionOriginalScale = Vector3.one;

    private bool isSelected, isHiding, isDragging = false;

    private void OnEnable()
    {
        isSelected = isHiding = isDragging = false;
    }

    private void Start()
    {
        boardController = BoardController.Instance;
    }

    private void OnMouseEnter()
    {
        if (!isHiding)
            potion.ChangeScale(potionMouseEnterScale);
    }

    private void OnMouseExit()
    {
        if (!isHiding)
            potion.ChangeScale(potionOriginalScale);
    }

    private void OnMouseDown()
    {
        if (!isSelected && !isHiding)
        {
            isSelected = true;
            originalMousePos = Input.mousePosition;
            boardController.SetSelectedTile(this);
        }
    }

    private void OnMouseDrag()
    {
        if (!isDragging && !isHiding)
        {
            var mouseInput = Input.mousePosition;
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 dragDirection = currentMousePos - originalMousePos;
            float deadzone = 15f;
            EMoveType moveType = EMoveType.Down;
            if (dragDirection.magnitude > deadzone)
            {
                if (Mathf.Abs(dragDirection.x) > Mathf.Abs(dragDirection.y))
                {
                    if (dragDirection.x > 0)
                        moveType = EMoveType.Right;
                    else
                        moveType = EMoveType.Left;
                }
                else
                {
                    if (dragDirection.y > 0)
                        moveType = EMoveType.Up;
                }

                boardController.DragTile(moveType);
                isDragging = true;
            }
        }
    }

    private void OnMouseUp()
    {
        potion.ChangeScale(potionOriginalScale);
        boardController.ReleasePotion();
        isSelected = false;
        isDragging = false;
    }

    public void HidePotion()
    {
        isHiding = true;
        potion.Hide(() =>
        {
            isHiding = false;
            potion = null;
        });
    }

    public void SetTileIndex(Vector2Int index)
    {
        tileIndex = index;
    }

    public void SetPotion(PotionController potion)
    {
        this.potion = potion;
        if (this.potion != null)
        {
            potion.transform.localPosition = transform.localPosition;
            potion.ChangeScale(potionOriginalScale);
        }
    }

    public void ChangePotion(PotionController potion)
    {
        this.potion = potion;
        if (this.potion != null)
        {
            MovePotion(transform.localPosition);
            potion.ChangeScale(potionOriginalScale);
        }
    }

    public void MovePotion(Vector3 targetPos)
    {
        potion.Move(targetPos);
    }
}
