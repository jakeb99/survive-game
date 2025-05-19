using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{

    private string fileName = "undead-defense-savefile";


    private static DataPersistenceManager instance;
    private GameData gameData;
    private List<IPersistentData> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager Instance { get { return instance; } }

    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("No saved game data found, creating new game");
            NewGame();
        }

        // push the loaded data to all the scripts that impleent IDataPersistence
        foreach (IPersistentData dataPersistentObject in dataPersistenceObjects)
        {
            dataPersistentObject.LoadGameData(gameData);
        }
    }

    public void LoadDataOnGamestart()
    {
        Debug.Log("Loading game data on start");

        dataPersistenceObjects = FindAllDataPersistenceObjects();

        // push the loaded data to all the scripts that impleent IDataPersistence
        foreach (IPersistentData dataPersistentObject in dataPersistenceObjects)
        {
            dataPersistentObject.LoadGameData(gameData);
        }
    }

    public void RestartGame()
    {

    }

    public void SaveGame()
    {
        dataPersistenceObjects.Clear();
        dataPersistenceObjects = FindAllDataPersistenceObjects();

        // extract the game data from objects that implement IPersistentData
        foreach (IPersistentData dataPersistentObject in dataPersistenceObjects)
        {
            dataPersistentObject.SaveGameData(ref gameData);
        }

        // save that data to a file using the specified handler
        dataHandler.Save(gameData);
    }

    private List<IPersistentData> FindAllDataPersistenceObjects()
    {
        IEnumerable<IPersistentData> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IPersistentData>();

        return new List<IPersistentData>(dataPersistenceObjects);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
