using System;

public class TileMouseUp
{
    public void MouseUp(IPotion potion, Action<bool> action)
    {
        if (potion != null)
            potion.ResetScale();
        action.Invoke(false);
    }
}
