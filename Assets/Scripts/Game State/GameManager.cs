using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPersistentData
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    public GameState CurrentState {  get; private set; }

    [field: SerializeField]public PlacementSystem PlacementSystem { get; private set; }
    [field: SerializeField]public UIManager UIManager { get; private set; }
    [field: SerializeField]public WaveManager WaveManager { get; private set; }
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }

    public Camera SceneCamera;
    public MeshRenderer PlaceableAreaMesh;
    public GameObject Player {  get; private set; }
    public string PlayerTag;
    public Action<int> OnUpdateScrapTotal;
    private DataPersistenceManager saveSystem;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }

        Player = GameObject.FindGameObjectWithTag(PlayerTag);
        CurrentState = new GameSetupState(this);
        saveSystem = FindAnyObjectByType<DataPersistenceManager>();
    }

    private void Start()
    {
        CurrentState.OnStateEnter();
        saveSystem.LoadDataOnGamestart();
    }

    public void ChangeState(GameState state)
    {
        CurrentState.OnStateExit();
        CurrentState = state;
        CurrentState.OnStateEnter();
    }

    public void OnStartWaveButtonPressed()
    {
        ChangeState(new GameWaveState(this));
    }

    public bool DecreaseScrap(int amount)
    {
        if (PlayerStats.TotalScrap - amount >= 0)
        {
            PlayerStats.TotalScrap -= amount;
            OnUpdateScrapTotal?.Invoke(PlayerStats.TotalScrap);
            return true;
        }
        return false;
    }

    public void IncreaseScrap(int amount)
    {
        PlayerStats.TotalScrap += amount;
        OnUpdateScrapTotal?.Invoke(PlayerStats.TotalScrap);
    }

    void IPersistentData.LoadGameData(GameData data)
    {
        this.PlayerStats = data.PlayerStats;
    }

    void IPersistentData.SaveGameData(ref GameData data)
    {
        data.PlayerStats = this.PlayerStats;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        saveSystem.SaveGame();
    }

    public void LoadLastSave()
    {
        saveSystem.LoadGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
