using System;
using UnityEngine;

public class TileMouseDrag
{
    public void MouseDrag(IPotion potion, BoardController boardController, Vector3 originalMousePos,
        bool isHiding, bool isDragging, Action<bool> isDraggingAct)
    {
        if (GameController.mouseInteract && !isDragging && !isHiding && potion != null)
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
                isDraggingAct.Invoke(true);
            }
        }
    }
}
