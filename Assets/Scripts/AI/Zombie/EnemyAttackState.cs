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
        }
        // cont tracking player
        enemy.currentTarget = enemy.player;
    }

    public override void OnStateUpdate()
    {
        if (!enemy.currentTarget)
        {
            enemy.ChangeState(new EnemyMoveToState(enemy));
        }
    }
}
