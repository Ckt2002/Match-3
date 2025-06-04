using System.Text;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public PotionController potion { get; private set; }
    public Vector2Int tileIndex { get; private set; }
    public Vector3 originalMousePos { get; set; } = Vector3.zero;
    public BoardController boardController { get; private set; }

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
        if (!isHiding && potion != null)
            potion.ZoomScale();
    }

    private void OnMouseExit()
    {
        if (!isHiding && potion != null)
            potion.ResetScale();
    }

    private void OnMouseDown()
    {
        if (!isSelected && !isHiding && potion != null)
        {
            isSelected = true;
            originalMousePos = Input.mousePosition;
            boardController.SetSelectedTile(this);
        }
    }

    private void OnMouseDrag()
    {
        if (!isDragging && !isHiding && potion != null)
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
        if (potion != null)
            potion.ResetScale();
        boardController.ReleaseTile();
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
            potion.ResetScale();
        }
    }

    public void ChangePotion(PotionController potion)
    {
        this.potion = potion;
        if (this.potion != null)
        {
            MovePotion(transform.localPosition);
            potion.ResetScale();
        }
    }

    public void MovePotion(Vector3 targetPos)
    {
        potion.Move(targetPos);
    }

    public void GetSpecialType(ESpecialType specialType)
    {
        StringBuilder specialName = new StringBuilder();
        specialName.Append(specialType.ToString());
        if (specialType != ESpecialType.Explosion &&
            specialType != ESpecialType.Lightning)
        {
            string newName = potion.potionType.ToString() + specialName.ToString();
            specialName.Clear().Append(newName);
        }

        isHiding = true;
        potion.Hide(() =>
        {
            isHiding = false;
            potion = null;
            ESpecial.TryParse(specialName.ToString(), ignoreCase: true, out ESpecial parsed);
            SetPotion(PoolingController.Instance.GetSpecial((int)parsed));
        });
    }
}
