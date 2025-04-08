using UnityEngine;

public class EnemyMoveToState : EnemyState
{
    
    public EnemyMoveToState(EnemyController enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered EnemyMoveToTargetState.");
        Debug.Log($"moving to {enemy.currentTarget.name}");
        enemy.agent.destination = enemy.currentTarget.transform.position;
        enemy.agent.isStopped = false;
    }

    public override void OnStateExit()
    {
        Debug.Log("Exited EnemyMoveToTargetState");
    }

    public override void OnStateUpdate()
    {
        if (enemy.agent.remainingDistance < 1.1f)
        {
            enemy.agent.isStopped = true;
            enemy.ChangeState(new EnemyAttackState(enemy));

        }
        enemy.agent.destination = enemy.currentTarget.transform.position;

        if (Physics.SphereCast(enemy.enemyEye.position,
            enemy.breakableCheckRadius,
            enemy.transform.forward, 
            out RaycastHit hit, 
            enemy.breakableCheckDistance, enemy.attackableLayerMask))
        {
            Debug.Log($"Hit breakable {hit.collider.name}");
            enemy.agent.isStopped = true;
            enemy.currentTarget = (hit.collider.name == "Player") ? hit.collider.gameObject : hit.collider.transform.parent.gameObject;      // barricades collider is in child but player collider is at root
            enemy.ChangeState(new EnemyAttackState(enemy));
        }
    }
}
