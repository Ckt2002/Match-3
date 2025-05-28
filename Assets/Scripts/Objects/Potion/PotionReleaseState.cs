public class PotionReleaseState : IPotionState
{
    PotionController controller;
    BoardController boardController;
    public PotionReleaseState(PotionController controller)
    {
        this.controller = controller;
        boardController = controller.boardController;
    }

    public void Enter()
    {
        boardController.ReleasePotion();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
