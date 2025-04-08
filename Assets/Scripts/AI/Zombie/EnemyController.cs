using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Invector.vCharacterController;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;

    public List<GameObject> targets;
    public GameObject currentTarget;

    public GameObject player;

    public NavMeshAgent agent;

    public Transform enemyEye;
    public float breakableCheckDistance;        // distance to check for breakable object in path
    public float breakableCheckRadius = 0.4f;
    public LayerMask attackableLayerMask;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //currentTarget = targets.Count > 0 ? targets[0] : null;
       
        currentState = new EnemyMoveToState(this);
    }

    private void Start()
    {
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

        //Gizmos.color = (Vector3.Distance(gameObject.transform.position, currentTarget.transform.position) <= attackDistance) ? Color.green : Color.red;
        //Debug.Log($"dist gizmo: {Vector3.Distance(gameObject.transform.position, currentTarget.transform.position)}");
        Gizmos.DrawLine(gameObject.transform.position, currentTarget.transform.position);
    }
}
