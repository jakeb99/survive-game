using UnityEngine;

public abstract class GameState
{
    protected GameManager gameManager;

    public GameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();

}
