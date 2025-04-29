using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    public GameState CurrentState {  get; private set; }

    [field: SerializeField]public PlacementSystem PlacementSystem { get; private set; }
    [field: SerializeField]public UIManager UIManager { get; private set; }
    [field: SerializeField]public WaveManager WaveManager { get; private set; }

    public Camera SceneCamera;
    public MeshRenderer PlaceableAreaMesh;
    public GameObject Player {  get; private set; }
    public string PlayerTag;

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
    }

    private void Start()
    {
        CurrentState.OnStateEnter();
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
}
