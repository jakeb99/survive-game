using UnityEngine;

public interface IDataPersistence
{
    void LoadGameData(GameData data);

    // pass by ref so we can directly write to the gamedata obj
    void SaveGameData(ref GameData data);
}
