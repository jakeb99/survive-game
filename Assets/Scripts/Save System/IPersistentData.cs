using UnityEngine;

public interface IPersistentData
{
    void LoadGameData(GameData data);

    void SaveGameData(ref GameData data);
}
