using System;
using UnityEngine;

public interface IPotion
{
    void Hide(Action action = null);
    void ZoomScale();
    void ResetScale();
    void ChangeScale(Vector3 scale);
    void Move(Vector3 targetPos);
    void ActiveSpecial(TileController tile,
        Vector3? startPos = null, Vector3? endPos = null);
}
