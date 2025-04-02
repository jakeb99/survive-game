using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;

    [SerializeField] private GameObject[] targets;
    [SerializeField] private GameObject currentTarget;

    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //currentState = new EnemyIdleState(this);
    }

    private void Start()
    {
        currentState.OnStateEnter();
    }

    private void Update()
    {
        currentState.OnStateUpdate();
    }

    private void ChangeState(EnemyState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }
}
