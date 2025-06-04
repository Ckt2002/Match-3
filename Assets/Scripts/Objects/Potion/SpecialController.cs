public class SpecialController : PotionController
{
    public ESpecialType specialType;

    public override void ActiveSpecial()
    {
        switch (specialType)
        {
            case ESpecialType.Explosion:
                break;

            case ESpecialType.Lightning:
                break;

            case ESpecialType.H:
                break;

            case ESpecialType.V:
                break;

            default:
                break;
        }
    }
}