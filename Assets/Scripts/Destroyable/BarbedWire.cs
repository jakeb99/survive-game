using UnityEngine;

public class BarbedWire : DestructableObject
{
    [SerializeField] float damageAmount;
    [SerializeField] float speedInBarbedWire;
    [SerializeField] AttackAbility attackAbility;
    private EnemyController enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.GetComponentInParent<EnemyController>();
            enemy.agent.speed = speedInBarbedWire;
            attackAbility.StartAttack(enemy.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            healthObj.DecrementHealth(damageAmount);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.agent.speed = enemy.initialAgentSpeed;
            attackAbility.StopAttack();
        }
    }

    public override void DestroyDestructable()
    {
        base.DestroyDestructable();
        enemy.agent.speed = enemy.initialAgentSpeed;
    }
}
