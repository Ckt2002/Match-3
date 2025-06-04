using System.Text;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [ContextMenu("Set Potion Type")]
    void SetPotionType()
    {
        EPotion.TryParse(gameObject.name, ignoreCase: true, out EPotion parsedType);
        GetComponent<PotionController>().potionType = parsedType;
    }

    [ContextMenu("Assign Specials")]
    void AssignSpecials()
    {
        string name = gameObject.name;
        StringBuilder potionName = new StringBuilder();
        StringBuilder specialType = new StringBuilder();
        potionName.Append(name);
        specialType.Append(name);
        if (!name.Contains("linghtning") && !name.Contains("explosion"))
        {
            potionName.Clear();
            specialType.Clear();
            var splits = name.Split('_');
            potionName.Append(splits[0]);
            specialType.Append(splits[1]);

            Debug.Log(splits[0]);
        }

        EPotion.TryParse(potionName.ToString(), ignoreCase: true, out EPotion parsedName);
        GetComponent<SpecialController>().potionType = parsedName;

        ESpecialType.TryParse(specialType.ToString(), ignoreCase: true, out ESpecialType parsedType);
        GetComponent<SpecialController>().specialType = parsedType;
    }
}
