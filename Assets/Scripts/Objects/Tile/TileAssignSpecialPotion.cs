using System;
using System.Text;

public class TileAssignSpecialPotion
{
    public void AssignSpecial(ESpecialType specialType, IPotion potion, EPotion potionType,
        Action<bool> hidingAct, Action afterHideAct, Action<PotionController, bool> setPotionAct)
    {
        StringBuilder specialName = new StringBuilder();
        specialName.Append(specialType.ToString());
        if (specialType != ESpecialType.Explosion &&
            specialType != ESpecialType.Lightning)
        {
            string newName = potionType.ToString() + specialName.ToString();
            specialName.Clear().Append(newName);
        }

        hidingAct.Invoke(true);
        potion.Hide(() =>
        {
            afterHideAct();
            ESpecial.TryParse(specialName.ToString(), ignoreCase: true, out ESpecial parsed);
            SpecialController potion = PoolingController.Instance.GetSpecial((int)parsed);
            setPotionAct(potion, true);
        });
    }
}
