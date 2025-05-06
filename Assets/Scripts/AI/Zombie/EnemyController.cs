using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;
    private Health healthObj;
    [SerializeField] private int enemyValue;
    public Rigidbody rb {  get; private set; }
    public float initialAgentSpeed;

    public GameObject currentTarget;

    public GameObject player;

    public NavMeshAgent agent;

    [field: SerializeField] public Animator animator {  get; private set; }
    [field: SerializeField] public float baseMoveSpeed {  get; private set; }
    [field: SerializeField] public float baseAnimSpeed { get; private set; }
    [SerializeField] AudioClip deathSFX;

    public Transform enemyEye;
    public float breakableCheckDistance;        // distance to check for breakable object in path
    public float breakableCheckRadius = 0.4f;
    public float attackRange;
    public LayerMask attackableLayerMask;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        healthObj = GetComponent<Health>();

        currentState = new EnemyMoveToState(this);
    } 

    private void Start()
    {
        player = GameManager.Instance.Player;
        agent.speed = initialAgentSpeed;
        healthObj.OnDeath += EnemyKilled;
        currentState.OnStateEnter();
    }

    private void Update()
    {
        SetAnimSpeed();
        currentState.OnStateUpdate();
        SetAnimSpeed();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    private void EnemyKilled()
    {
        Debug.Log($"{gameObject.name} was killed!");
        PlayDeathSound();
        gameObject.SetActive(false);
        GameManager.Instance.WaveManager.spawnedEnemies.Remove(gameObject);
        GameManager.Instance.PlayerStats.TotalKills++;
        GameManager.Instance.UIManager.UpdateTotalKillsText();
        GameManager.Instance.IncreaseScrap(enemyValue);
        Destroy(gameObject);
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyEye.position, breakableCheckRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * breakableCheckDistance, breakableCheckRadius);
        Gizmos.DrawLine(enemyEye.position, enemyEye.position + enemyEye.forward * breakableCheckDistance);

        if (currentTarget != null)
            Gizmos.DrawLine(gameObject.transform.position, currentTarget.transform.position);
    }

    private void SetAnimSpeed()
    {
        float currentSpeed = agent.velocity.magnitude;
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
        if (currentState.GetType() == typeof(EnemyMoveToState))
        {
            float speedMult = currentSpeed / baseMoveSpeed;

            animator.speed = speedMult * baseAnimSpeed;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    private void PlayDeathSound()
    {
        GameObject tempAudio = new GameObject("tempAudio");
        tempAudio.transform.position = gameObject.transform.position;
        AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
        tempAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        tempAudioSource.spatialBlend = 1f;
        tempAudioSource.PlayOneShot(deathSFX);
        Destroy(tempAudio, deathSFX.length);
    }
}
