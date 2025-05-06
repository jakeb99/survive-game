using System.Collections.Generic;
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

        TogglePlacedTurretsRangeIndicatorShown(true);
    }

    public override void OnStateExit()
    {
        // save the game
        gameManager.SaveGame();

        // hide shop UI
        gameManager.UIManager.HideShopUI();
        gameManager.UIManager.HideStartWaveButton();
        // turn off abiliy to place items
        gameManager.PlaceableAreaMesh.enabled = false;
        // call NavMeshManager and bake the nav mesh surface
        TogglePlacedTurretsRangeIndicatorShown(false);
        NavMeshManager.Instance.BakeNavMesh();
    }

    public override void OnStateUpdate()
    {
       
    }

    /// <summary>
    /// Toggle if the range indicator is shown
    /// </summary>
    /// <param name="isShown">shown if true</param>
    private void TogglePlacedTurretsRangeIndicatorShown(bool isShown)
    {
        TurretController[] placedTurrets = GameObject.FindObjectsByType<TurretController>(FindObjectsSortMode.None);

        foreach (TurretController placedTurret in placedTurrets)
        {
            placedTurret.ShowRangeIndicator(isShown);
        }
    }
}
