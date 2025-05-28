using UnityEngine;

public class Tool : MonoBehaviour
{
    [ContextMenu("Set Potion Type")]
    void SetPotionType()
    {
        EPotionType.TryParse(gameObject.name, ignoreCase: true, out EPotionType parsedType);
        GetComponent<PotionController>().potionType = parsedType;
    }
}
