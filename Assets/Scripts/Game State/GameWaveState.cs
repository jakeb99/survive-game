using UnityEngine;

public class GameWaveState : GameState
{
    public GameWaveState(GameManager gameManager) : base(gameManager)
    {
        gameManager.WaveManager.OnWaveEnd += GoToSetUpState;
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

    private void GoToSetUpState()
    {
        gameManager.ChangeState(new GameSetupState(gameManager));
    }
}
