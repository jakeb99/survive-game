using UnityEngine;

public class GameWaveState : GameState
{
    public GameWaveState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Wave State");
        // init wave system
    }

    public override void OnStateExit()
    {

        Debug.Log("Exited Wave State");
    }

    public override void OnStateUpdate()
    {
        
    }
}
