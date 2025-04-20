using UnityEngine;

public class GameSetupState : GameState
{
    public GameSetupState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered GameSetupState");
        // show shop UI
        gameManager.UIManager.ShowShopUI();
        // allow building / placing items
    }

    public override void OnStateExit()
    {
        // hide shop UI
        gameManager.UIManager.HideShopUI();
        // turn off abiliy to place items
        // call NavMeshManager and bake the nav mesh surface
        NavMeshManager.Instance.BakeNavMesh();
    }

    public override void OnStateUpdate()
    {
        // wait for the start night button to be pressed then go to the wave state
    }
}
