using UnityEngine;

public class PotionDragState : IPotionState
{
    Vector3 originalMousePos;
    BoardController boardController;

    public PotionDragState(PotionController potion)
    {
        originalMousePos = potion.originalMousePos;
        boardController = potion.boardController;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        var mouseInput = Input.mousePosition;
        Vector3 currentMousePos = Input.mousePosition;
        Vector3 dragDirection = currentMousePos - originalMousePos;
        float deadzone = 10f;
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

            boardController.DragPotion(moveType);
        }
    }

    public void Exit()
    {
        boardController = null;
    }
}