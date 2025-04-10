using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private AttackAbility attackAbility;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        attackAbility = enemy.GetComponent<AttackAbility>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Attack State");
        if (attackAbility)
        {
            attackAbility.StartAttack(enemy.currentTarget);
            enemy.rb.isKinematic = true;
        } else
        {
            Debug.Log("No AttackAbility found");
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("Exited Attack State");
        if (attackAbility)
        {
            attackAbility.StopAttack();
            enemy.rb.isKinematic = false;
        }        
    }

    public override void OnStateUpdate()
    {
        if (!enemy.currentTarget)
        {
            enemy.ChangeState(new EnemyMoveToState(enemy));
        } else
        {
            // prevent attacking player if out of range
            if (enemy.currentTarget == enemy.player && Vector3.Distance(enemy.transform.position, enemy.player.transform.position) > enemy.attackRange)
            {
                enemy.ChangeState(new EnemyMoveToState(enemy));
            }
        }
    }
}
