using UnityEngine;

public class GameWaveState : GameState
{
    public GameWaveState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Wave State");
        gameManager.WaveManager.StartWave();
    }

    public override void OnStateExit()
    {

        Debug.Log("Exited Wave State");
    }

    public override void OnStateUpdate()
    {
        
    }
}
