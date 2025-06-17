using System;
using UnityEngine;

public class TileMouseDown
{
    public void MouseDown(IPotion potion, BoardController boardController, TileController tileController,
        bool isSelected, bool isHiding, Action<Vector3> mousePosAct)
    {
        if (GameController.mouseInteract && !isSelected && !isHiding && potion != null)
        {
            isSelected = true;
            mousePosAct.Invoke(Input.mousePosition);
            boardController.SetSelectedTile(tileController);
        }
    }
}
