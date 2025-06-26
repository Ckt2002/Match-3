public class NotificationUI : UI
{
    public override void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public override void HideUI()
    {
        gameObject.SetActive(false);
    }
}