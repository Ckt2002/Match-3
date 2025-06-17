public class TileMouseExit
{
    public void MouseExit(IPotion potion, bool isHiding)
    {
        if (GameController.mouseInteract && !isHiding && potion != null)
            potion.ResetScale();
    }
}
