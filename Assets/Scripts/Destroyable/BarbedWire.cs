using UnityEngine;

public class BarbedWire : DestructableObject
{
    [SerializeField] float damageAmount;
    [SerializeField] float speedInBarbedWire;
    private EnemyController enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.GetComponentInParent<EnemyController>();
            enemy.agent.speed = speedInBarbedWire;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            healthObj.DecrementHealth(damageAmount);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy.agent.speed = enemy.initialAgentSpeed;
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        enemy.agent.speed = enemy.initialAgentSpeed;
    }
}
