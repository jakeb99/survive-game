
[System.Serializable]
public class GameData
{
    public PlayerStats PlayerStats;

    // constructor contains new game data (i.e., when no load data present)
    public GameData()
    {
        this.PlayerStats = new PlayerStats();
    }
}
