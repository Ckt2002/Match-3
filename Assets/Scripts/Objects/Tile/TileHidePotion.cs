using System;

public class TileHidePotion
{
    public void HidePotion(IPotion potion, bool isHiding, Action<bool> hidingAct, Action afterHideAct)
    {
        if (!isHiding && potion != null)
        {
            hidingAct.Invoke(true);
            potion.Hide(afterHideAct);
        }
    }
}
