public class TileMouseEnter
{
    public void MouseEnter(IPotion potion, bool isHiding)
    {
        if (GameController.mouseInteract && !isHiding && potion != null)
            potion.ZoomScale();
        else if ((!GameController.mouseInteract || isHiding) && potion != null)
            potion.ResetScale();
    }
}
