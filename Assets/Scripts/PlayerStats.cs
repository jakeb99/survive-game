using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int TotalScrap;
    public int HighestWave;
    public int TotalBulletsShot;
    public int TotalDamageDelt;
    public int TotalScrapSpent;
    public int TotalKills;

    // default values
    public PlayerStats()
    {
        TotalScrap = 400;
        HighestWave = 0;
        TotalBulletsShot = 0;
        TotalDamageDelt = 0;
        TotalScrapSpent = 0;
        TotalKills = 0;
    }
}
