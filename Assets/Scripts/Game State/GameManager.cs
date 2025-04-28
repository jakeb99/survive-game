using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }
    public GameState CurrentState {  get; private set; }

    public PlacementSystem PlacementSystem;
    public UIManager UIManager;
    public Camera SceneCamera;
    public MeshRenderer PlaceableAreaMesh;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
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
