using UnityEngine;

public class PotionStateMachine : MonoBehaviour
{
    private IPotionState state;

    public void ChangeState(IPotionState state)
    {
        if (this.state != null)
            this.state.Exit();
        this.state = state;
        this.state.Enter();
    }

    private void Update()
    {
        if (state != null)
            state.Execute();
    }
}
