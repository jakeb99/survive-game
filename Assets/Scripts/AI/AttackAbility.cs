using System;
using UnityEngine;

public class AttackAbility : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private AudioClip attackSound;

    private Health attackTargetHealthObj;
    private bool isAttacking;
    private float timer = 0;

    public Action OnAttack;

    private void Update()
    {
        if (isAttacking)
        {
            // start attack timer
            timer += Time.deltaTime;

            if (timer >= attackCoolDown)
            {
                Attack();
                timer = 0;
            }
        }
    }

    public void StartAttack(GameObject target)
    {
        attackTargetHealthObj = target.GetComponent<Health>();
        isAttacking = true;
        Debug.Log($"Attakcing {target.name}");
    }

    private void Attack()
    {
        OnAttack?.Invoke();
        if (attackTargetHealthObj)
        {
            attackTargetHealthObj.DecrementHealth(damageAmount);
        } else
        {
            Debug.Log($"No health object found.");
        }
    }

    public void StopAttack()
    {
        isAttacking = false;
    }
}
