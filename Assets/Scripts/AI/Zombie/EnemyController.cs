using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;
    public Rigidbody rb {  get; private set; }
    public float initialAgentSpeed;

    public GameObject currentTarget;

    public GameObject player;

    public NavMeshAgent agent;

    public Transform enemyEye;
    public float breakableCheckDistance;        // distance to check for breakable object in path
    public float breakableCheckRadius = 0.4f;
    public float attackRange;
    public LayerMask attackableLayerMask;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        currentState = new EnemyMoveToState(this);
    } 

    private void Start()
    {
        agent.speed = initialAgentSpeed;
        currentState.OnStateEnter();
    }

    private void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
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
}
