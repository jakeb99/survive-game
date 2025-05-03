using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public PlayerStats PlayerStats;
    public int CurrentWave;
    public List<PlaceableObjectSerializableData> PlacedObjects;

    // constructor contains new game data (i.e., when no load data present)
    public GameData()
    {
        this.PlayerStats = new PlayerStats();
        this.CurrentWave = 1;
        this.PlacedObjects = new List<PlaceableObjectSerializableData>();
    }
}
