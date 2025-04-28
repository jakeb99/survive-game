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
        gameManager.UIManager.ShowStartWaveButton();
        // enable blue area to highlight where player can build
        gameManager.PlaceableAreaMesh.enabled = true;
        // allow building / placing items
    }

    public override void OnStateExit()
    {
        // hide shop UI
        gameManager.UIManager.HideShopUI();
        gameManager.UIManager.HideStartWaveButton();
        // turn off abiliy to place items
        gameManager.PlaceableAreaMesh.enabled = false;
        // call NavMeshManager and bake the nav mesh surface
        NavMeshManager.Instance.BakeNavMesh();
    }

    public override void OnStateUpdate()
    {
        // wait for the start night button to be pressed then go to the wave state
    }
}
