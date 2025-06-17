using System;
using UnityEngine;

public interface IPotion
{
    void Hide(Action action = null);
    void ZoomScale();
    void ResetScale();
    void ChangeScale(Vector3 scale);
    void Move(Vector3 targetPos);
    void ActiveSpecial(TileController tile);
}
