using UnityEngine;

public class GameEndState : GameState
{
    public GameEndState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Game End State");
        gameManager.UIManager.UpdateGameOverStats();
        gameManager.UIManager.ShowGameOverScreen();
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting Game End State");
        gameManager.UIManager.HideGameOverScreen();
    }

    public override void OnStateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
