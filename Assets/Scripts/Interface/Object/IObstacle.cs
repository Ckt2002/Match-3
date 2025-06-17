using System;

public interface IObstacle
{
    void RegisterAction(Action action);
    void ActiveObstacle();
    void TakeDamage();
    void HideObstacle();
}